using grpc_service_app.Interceptors;
using grpc_service_app.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(option =>
{
    option.EnableDetailedErrors = true;
    option.Interceptors.Add<ExceptionInterceptor>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ConverterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();