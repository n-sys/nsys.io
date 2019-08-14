using System;
using System.Collections.Generic;
using Grpc.Core;
using Nsys.Api;       // Used for echo requests 

namespace EchoDemo
{
    /// <summary>
    /// This is a demo program which simply sends and echo request to the NSYS API
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

            // create the nsys Diagnostic service client
            var client = new Diagnostic.DiagnosticClient(channel);

            // get the request ready
            var request = new EchoRequest { Text = "Hello nsys! from C#" };

            // you must add the api key to the request headers
            var requestHeaders = new Metadata {{ "x-api-key", apiKey }};

            // make the request and get the reply
            var reply = client.Echo(request, requestHeaders);

            // do something to the reply
            Console.WriteLine("reply: " + reply);

            channel.ShutdownAsync().Wait();
        }
    }
}
