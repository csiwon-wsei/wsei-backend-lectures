using System.Diagnostics.Metrics;

namespace grpc_service_app.Services;

public enum LenghtUnit
{
    Kilometer = 1000 * (Meter),
    Meter = 100 * (Centimeter),
    Centimeter = 10 * (Milimeter),
    Milimeter = 1000 * (Micrometr),
    Micrometr = 1
}