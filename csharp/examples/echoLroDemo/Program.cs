using System;
using System.Collections.Generic;
using System.Threading;
using Grpc.Core;
using Google.LongRunning;
using Nsys.Api;       // Used for echo requests 

namespace EchoLroDemo
{
    /// <summary>
    /// This is a demo program showing how to use a LRO (LongRunning Operation)
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


            // create the diagnostic client
            var diagClient = new Diagnostic.DiagnosticClient(channel);

            // create the longrunning operations client
            var opClient = new Operations.OperationsClient(channel);

            // make our echo request (same as the regular version)
            var request = new EchoRequest { Text = "an example LRO, from C#" };

            // The request headers must contain the api key
            var requestHeaders = new Metadata {{ "x-api-key", apiKey }};

            // make the request
            var lro = diagClient.EchoLRO(request, requestHeaders);

            
            // poll the operation until it finishes or till we timeout
            var startTime = DateTime.Now;
            while (!lro.Done)
            {
                Thread.Sleep(50);
                lro = opClient.GetOperation(new GetOperationRequest { Name = lro.Name }, requestHeaders);
                if ((DateTime.Now - startTime).TotalMilliseconds > 5000)
                {
                    Console.WriteLine("Timeout waiting for EchoLRO to finish");
                    channel.ShutdownAsync().Wait();
                    return;
                }
            }

            // When a LRO is done, either the Error or the Response is populated so check error first:
            if (lro.Error != null)
            {
                Console.WriteLine("Error in EchoLRO: " + lro.Error);
                channel.ShutdownAsync().Wait();
                return;
            }
            
            // we're all good here so unpack the response and be done
            var response = lro.Response.Unpack<EchoResponse>();
            Console.WriteLine("EchoLRO response: " + response);

            channel.ShutdownAsync().Wait();                
        }
    }
}
