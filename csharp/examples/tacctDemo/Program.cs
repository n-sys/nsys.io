using System;
using System.Collections.Generic;
using System.Threading;
using Grpc.Core;
using Google.LongRunning;
using Nsys.Api;       // Used for echo requests 
using Nsys.Api.Tacct; // Used for the Tickets and Accounts services

namespace TacctDemo
{
    /// <summary>
    /// This is a demo program showing how to use the Nsys Api
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("You must pass the api key in as the first argument to this program.");
                return;
            }
            var apiKey = args[0];

            // first we create a grpc channel connected to our host over tls
            var host = "api.nsys.io";
            var port = 39111;
            var creds = new SslCredentials();
            var channelOptions = new List<ChannelOption>
            {
                new ChannelOption(ChannelOptions.SslTargetNameOverride, host)
            };
            var channel = new Channel(host, port, creds, channelOptions);

            // try an echo request (defined below)
            Echo(channel, apiKey, "what's up nsys?");
            
            // try an echo request as a longrunning operation (defined below)
            EchoLRO(channel, apiKey, "this is a LRO!", 50, 5000);

            // create a new subaccount (defined below)
            var subAccount = CreateSubaccount(channel, apiKey, "my throwaway test subaccount");
            
            // create a new ticket (api key) for the new subaccount (defined below)
            var newTicket = CreateTicket(channel, apiKey, subAccount.Account.Id, 2);
            
            // send an echo request as the subaccount using the new ticket
            Echo(channel, newTicket.Ticket.ApiKey, "From my new subaccount!");

            // List subaccounts under our main account (defined below)
            ListSubaccounts(channel, apiKey);
            
            // List tickets under our main account (defined below)
            ListTickets(channel, apiKey, "");

            // List tickets under our new sub account
            ListTickets(channel, apiKey, subAccount.Account.Id);

            // delete the new subaccount (defined below)
            DeleteAccount(channel, apiKey, subAccount.Account.Id);
            
            // List subaccounts under our main account to see the new account is no longer active
            ListSubaccounts(channel, apiKey);

            channel.ShutdownAsync().Wait();
        }

        /// <summary>
        /// Sends an echo request to the nsys api.
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="message">The message that will be echoed.</param>
        static void Echo(Channel channel, string apiKey, string message)
        {
            var diagClient = new Diagnostic.DiagnosticClient(channel);
            var request = new EchoRequest { Text = message };
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var reply = diagClient.Echo(request, requestHeaders);
            Console.WriteLine("synchro Echo response: " + reply.Text);
        }

        /// <summary>
        /// Sends an echo request to the nsys api. This is just an example of how you 
        /// might poll for responses from a LRO.
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="message">The message that will be echoed.</param>
        /// <param name="pollTime">How many milliseconds to wait between polls.</param>
        /// <param name="timeout">Timeout in number of milliseconds.</param>
        static void EchoLRO(Channel channel, string apiKey, string message, int pollTime, int timeout)
        {
            var diagClient = new Diagnostic.DiagnosticClient(channel);
            var opClient = new Operations.OperationsClient(channel);
            var request = new EchoRequest { Text = message };
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var lro = diagClient.EchoLRO(request, requestHeaders);

            var startTime = DateTime.Now;
            while (!lro.Done)
            {
                Thread.Sleep(pollTime);
                lro = opClient.GetOperation(new GetOperationRequest { Name = lro.Name }, requestHeaders);
                if ((DateTime.Now - startTime).TotalMilliseconds > timeout)
                {
                    Console.WriteLine("Timeout on EchoLRO!");
                    return;
                }
            }

            if (lro.Error != null)
            {
                Console.WriteLine("Error in EchoLRO: " + lro.Error);
                return;
            }

            var response = lro.Response.Unpack<EchoResponse>();
            Console.WriteLine("EchoLRO finished, response text: " + response.Text);
        }

        /// <summary>
        /// Creates a ticket for the given account.
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host.</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="accountID">The account the ticket will belong to, 
        /// if empty will belong to whoever owns the apikey.</param>
        /// <param name="nHoursToLive">Number of hours the api key will live.</param>
        /// <returns>The raw response from the CreateTicket request.</returns>
        static CreateTicketResponse CreateTicket(Channel channel, string apiKey, string accountID, int nHoursToLive)
        {
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var ticketClient = new Tickets.TicketsClient(channel);
            var expireDate = DateTimeOffset.Now.AddHours(nHoursToLive).ToUnixTimeSeconds();
            var createTicketRequest = new CreateTicketRequest
            {
                AccountId = accountID,
                ExpireTime = new Google.Protobuf.WellKnownTypes.Timestamp { Seconds = expireDate }
            };
            var createTicketResponse = ticketClient.CreateTicket(createTicketRequest, requestHeaders);
            return createTicketResponse;
        }

        /// <summary>
        /// Creates a new subaccount
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host.</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="description">Description of the new subaccount.</param>
        /// <returns>The raw response from the CreateAccountRequest.</returns>
        static CreateAccountResponse CreateSubaccount(Channel channel, string apiKey, string description)
        {
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var accountsClient = new Accounts.AccountsClient(channel);
            var newAccountRequest = new CreateAccountRequest{Description = description};
            var createAccountResponse = accountsClient.CreateAccount(newAccountRequest, requestHeaders);
            return createAccountResponse;
        }

        /// <summary>
        /// List all the subaccounts under the given api key.
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host.</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        static void ListSubaccounts(Channel channel, string apiKey)
        {
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var accountsClient = new Accounts.AccountsClient(channel);
            var response = accountsClient.ListAccounts(new ListAccountsRequest(), requestHeaders);
            foreach (var account in response.Accounts) {
                Console.WriteLine(account);
            }
        }

        /// <summary>
        /// Lists all the tickets for the given accountID.  If the accountID is empty it
        /// returns the tickets for the main account.
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host.</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="accountID">The account ID</param>
        static void ListTickets(Channel channel, string apiKey, string accountID)
        {
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var ticketsClient = new Tickets.TicketsClient(channel);
            var request = new ListTicketsRequest { AccountId = accountID };
            var response = ticketsClient.ListTickets(request, requestHeaders);
            Console.WriteLine("Tickets for account " + accountID);
            foreach (var tickets in response.Tickets)
            {
                Console.WriteLine(tickets);
            }
        }

        /// <summary>
        /// Deletes the given subaccount
        /// </summary>
        /// <param name="channel">The channel connected to the NSYS host.</param>
        /// <param name="apiKey">The API Key aka the ticket.</param>
        /// <param name="accountID">The account ID</param>
        static void DeleteAccount(Channel channel, string apiKey, string accountID)
        {
            var requestHeaders = new Metadata { { "x-api-key", apiKey } };
            var accountsClient = new Accounts.AccountsClient(channel);
            var request = new DeleteAccountRequest { AccountId = accountID };
            accountsClient.DeleteAccount(request, requestHeaders);
            Console.WriteLine("Account " + accountID + " deleted");
        }
    }
}
