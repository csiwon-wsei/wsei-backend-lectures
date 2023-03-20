// See https://aka.ms/new-console-template for more information

using grpc_service_app.message.v1;
using grpc_service_app.converter.v1;
using Grpc.Net.Client;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5229");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

var converterClient = new Converter.ConverterClient(channel);
var request = new InputRequest{Value = 10, Unit = "kl"};
try
{
    var response = converterClient.Convert(request);
    Console.WriteLine("Converter");
    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}