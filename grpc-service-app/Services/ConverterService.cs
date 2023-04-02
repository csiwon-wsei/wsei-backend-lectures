using Grpc.Core;
using grpc_service_app.converter.v1;

namespace grpc_service_app.Services;

public class ConverterService : Converter.ConverterBase
{
    private readonly ILogger<ConverterService> _logger;

    public ConverterService(ILogger<ConverterService> logger)
    {
        _logger = logger;
    }

    public override async Task ConvertStream(
        IAsyncStreamReader<InputRequest> requestStream,
        IServerStreamWriter<OutputReply> responseStream,
        ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            var unit = Enum.Parse<LenghtUnit>(request.Unit);
            double v1 = (int) unit;
            double v2 = (int) LenghtUnit.Meter;
            await responseStream.WriteAsync(new OutputReply()
            {
                Value = (request.Value * v1) / v2,
                Unit = LenghtUnit.Meter.ToString()
            });
        }
    }
    
    public override Task<OutputReply> Convert(InputRequest request, ServerCallContext context)
    {
        var unit = Enum.Parse<LenghtUnit>(request.Unit);
        double v1 = (int) unit;
        double v2 = (int) LenghtUnit.Meter; 
        return Task.FromResult(
            new OutputReply()
            {
                Value = (request.Value * v1) / v2,
                Unit = LenghtUnit.Meter.ToString()
            });
    }

}