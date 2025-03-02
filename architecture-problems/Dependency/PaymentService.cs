namespace architecture_problems;

public class PaymentService
{
    private IDateTimeProvider provider;

    public PaymentService(IDateTimeProvider provider)
    {
        this.provider = provider;
    }

    decimal InvalidCalculatePayment(CarRent rent)
    {
        var span = rent.RentDateOnly.ToDateTime(new TimeOnly(0,0,0)) - DateTime.Now;
        return Convert.ToDecimal(span.TotalDays) * rent.PricePerDay;
    }
    
    decimal ValidCalculatePayment(DateTime now, CarRent rent)
    {
        var span = rent.RentDateOnly.ToDateTime(new TimeOnly(0,0,0)) - now;
        return Convert.ToDecimal(span.TotalDays) * rent.PricePerDay;
    }

    decimal CalculatePayment(CarRent rent)
    {
        var span = rent.RentDateOnly.ToDateTime(new TimeOnly(0,0,0)) - provider.Now;
        return Convert.ToDecimal(span.TotalDays) * rent.PricePerDay;
    }
    
}