using architecture_problems;

/// <summary>
/// Klasa zachowuje SRP
/// Zmiana notacji numeru rejestracyjnego nie wpływa na kod klasy ValidCarRent
/// Należy zmienić klasę PlateNumber, której odpowiedzialność
/// </summary>
class ValidCarRent
{
    public DateOnly RentDateOnly { get; init; }
    
    public decimal PricePerDay { get; init; }

    public PlateNumber? PlateNumber { get; init; }
}