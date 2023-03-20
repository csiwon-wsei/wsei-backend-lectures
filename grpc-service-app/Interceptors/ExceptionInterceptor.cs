using System.Reflection.Metadata;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace grpc_service_app.Interceptors;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger _logger;

    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            _logger.LogInformation("Interceptor async");
            return await continuation(request, context);
        }
        catch (ArgumentException e)
        {
            _logger.LogInformation("Interceptor exception thrown");
            Status status = new Status(StatusCode.InvalidArgument, $"Unknown unit {e.ParamName}!");
            throw new RpcException(status, e.Message); 
        }
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        try
        {
            _logger.LogInformation("Interceptor Async");
            var call = continuation(request, context);
            return call;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Interceptor exception thrown");
            Status status = new Status(StatusCode.InvalidArgument, "Unknown unit!");
            throw new RpcException(status, e.Message); 
        }
    }
}