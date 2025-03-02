namespace architecture_problems;

public class CarRent
{
    private readonly Dictionary<char?, string> _provinces = new()
    {
        { 'K' ,"małopolskie"},
        { 'T', "świętokrzyskie"},
        { 'J', "małopolskie"}
    };
    public DateOnly RentDateOnly { get; init; }
    
    public decimal PricePerDay { get; init; }

    public string? PlateNumber { get; init; }
    
    public string ProvinceFromPlateNumber => _provinces[PlateNumber?.Split(" ")[0][0]];
}