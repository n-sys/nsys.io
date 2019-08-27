using System;
using System.Collections.Generic;
using Grpc.Core;
using Nsys.Api.Tacct; // Used for the Tickets and Accounts services

namespace TacctDemo
{
    /// <summary>
    /// This is a CLI program which can add, delete, and list accounts and tickets.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Usage prints the program's usage and then exits with a code of 1
        /// </summary>
        static void Usage()
        {
            var usage = @"Usage:
  [command] [command args...]

  The API key must be in the NSYS_TICKET environment variable.

    Available Commands:

      listAccounts
      addAccount    [optional description]
      deleteAccount [accountId]

      listTickets  [optional accountId]
      addTicket    [hoursToLive] [optional accountId]
      deleteTicket [ticket]";

            Console.WriteLine(usage);
            Environment.Exit(1);
        }

        /// <summary>
        /// Main simply finds which command needs to be run and passes all the
        /// arguments to the corresponding method for it to deal with.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 1) Usage();
            if (GetApiKey() == "")
            {
                Console.WriteLine("Missing API Key!");
                Usage();
            }

            switch (args[0])
            {
                case "listAccounts":
                    ListAccounts(args);
                    break;
                case "addAccount":
                    AddAccount(args);
                    break;
                case "deleteAccount":
                    DeleteAccount(args);
                    break;
                case "listTickets":
                    ListTickets(args);
                    break;
                case "addTicket":
                    AddTicket(args);
                    break;
                case "deleteTicket":
                    DeleteTicket(args);
                    break;
                default:
                    Usage();
                    break;
            }
        }

        /// <summary>
        /// ListAccounts lists all accounts owned by the owner of the api key.
        /// </summary>
        static void ListAccounts(string[] args)
        {
            var chan = GetGrpcChannel();
            var client = new Accounts.AccountsClient(chan);

            var list = client.ListAccounts(new ListAccountsRequest { }, ReqHead());
            foreach (var item in list.Accounts) Console.WriteLine(item);
            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// AddAccount adds a subaccount. Can take an optional description.
        /// TODO: add optional method filters (for now it inherits filters from
        ///       the master account)
        /// </summary>
        static void AddAccount(string[] args)
        {
            var description = "";
            if (args.Length > 1) description = args[1];

            var chan = GetGrpcChannel();
            var client = new Accounts.AccountsClient(chan);

            var req = new CreateAccountRequest { Description = description };
            var resp = client.CreateAccount(req, ReqHead());
            Console.WriteLine(resp);
            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// DeleteAccount deletes the given account.
        /// </summary>
        static void DeleteAccount(string[] args)
        {
            if (args.Length < 2) Usage();
            var accountId = args[1];

            var chan = GetGrpcChannel();
            var client = new Accounts.AccountsClient(chan);

            var req = new DeleteAccountRequest { AccountId = accountId };
            client.DeleteAccount(req, ReqHead());

            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// ListTickets lists all the tickets in the main account owned by the 
        /// api key, or if available, lists the tickets in the optional sub account.
        /// </summary>
        static void ListTickets(string[] args)
        {
            var accountId = "";
            if (args.Length > 1) accountId = args[1];

            var chan = GetGrpcChannel();
            var client = new Tickets.TicketsClient(chan);

            var req = new ListTicketsRequest { AccountId = accountId };
            var list = client.ListTickets(req, ReqHead());
            foreach (var item in list.Tickets) Console.WriteLine(item);

            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// AddTicket adds a ticket with the given hours to live. The ticket will
        /// be added to the account owned by the api key, or if available, the
        /// optional sub account.
        /// </summary>
        static void AddTicket(string[] args)
        {
            if (args.Length < 2) Usage();
            var hours2live = double.Parse(args[1]);
            var accountId = "";
            if (args.Length > 2) accountId = args[2];

            var chan = GetGrpcChannel();
            var client = new Tickets.TicketsClient(chan);

            var expireDate = DateTimeOffset.Now.AddHours(hours2live).ToUnixTimeSeconds();
            var req = new CreateTicketRequest
            {
                AccountId = accountId,
                ExpireTime = new Google.Protobuf.WellKnownTypes.Timestamp { Seconds = expireDate }
            };
            var resp = client.CreateTicket(req, ReqHead());
            Console.WriteLine(resp);

            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// DeleteTicket deletes the given ticket.
        /// </summary>
        static void DeleteTicket(string[] args)
        {
            if (args.Length < 2) Usage();
            var ticket = args[1];

            var chan = GetGrpcChannel();
            var client = new Tickets.TicketsClient(chan);

            var req = new DeleteTicketRequest { ApiKey = ticket };
            client.DeleteTicket(req, ReqHead());

            chan.ShutdownAsync().Wait();
        }

        /// <summary>
        /// Returns the ApiKey from the environment
        /// </summary>
        static string GetApiKey()
        {
            return Environment.GetEnvironmentVariable("NSYS_TICKET");
        }

        /// <summary>
        /// Returns a Metadata request header with the api key
        /// </summary>
        static Metadata ReqHead()
        {
            return new Metadata { { "x-api-key", GetApiKey() } };
        }

        /// <summary>
        /// GetGrpcChannel returns a grpc channel connected to the nsys server.
        /// It uses tls signed by the server side and sets the message length limits.
        /// </summary>
        static Channel GetGrpcChannel()
        {
            var host = "api.nsys.io";
            var port = 39111;
            var channelOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.SslTargetNameOverride, host),
                new ChannelOption(ChannelOptions.MaxReceiveMessageLength, 50*1024*1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, 50*1024*1024),
            };
            return new Channel(host, port, new SslCredentials(), channelOptions);
        }
    }
}
