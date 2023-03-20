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

    public override Task<OutputReply> Convert(InputRequest request, ServerCallContext context)
    {
        var unit = Enum.Parse<LenghtUnit>(request.Unit);
        double result = request.Value * ((double) unit) / (double) LenghtUnit.Meter;
        return Task.FromResult(
            new OutputReply()
            {
                Value = result,
                Unit = LenghtUnit.Meter.ToString()
            });
    }

}