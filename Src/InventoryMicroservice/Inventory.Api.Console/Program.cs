namespace Inventory.Api.Console
{
    using Grpc.Net.Client;
    using GrpcNamespace;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
             using GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");

            //var client = new GrpcNamespace.Greeter

            var client = new GrpcNamespace.Greeter.GreeterClient(channel);

            HelloReply replay = client.SayHello(new HelloRequest());

            var message = replay.Message;
        }
    }
}
