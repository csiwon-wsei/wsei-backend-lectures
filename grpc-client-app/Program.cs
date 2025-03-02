// See https://aka.ms/new-console-template for more information

using grpc_client_app.converter.v1;
using Grpc.Core;
using Grpc.Net.Client;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5229");

// Wywołanie metody unarnej
var converterClient = new Converter.ConverterClient(channel);

var request = new InputRequest{Value = 100, Unit = "Milimeter"};
try
{
    var response = converterClient.Convert(request);
    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
// Wywołanie metody dwukierunkowej strumieniowej
var call = converterClient.ConvertStream();
// zadanie będzie wywoływane asynchroniecznie dla każdej odpowiedzi żądania
var readTask = Task.Run(async () =>
{
    await foreach (var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"Received response: {response}");
    }
});

while (true)
{
    var result = Console.ReadLine();
    if (string.IsNullOrEmpty(result))
    {
        break;
    }

    InputRequest inputRequest = new InputRequest() {Unit = "Centimeter", Value = Random.Shared.Next(1, 200)};
    Console.WriteLine($"Sending request: {inputRequest}");
    await call.RequestStream.WriteAsync(inputRequest);
}