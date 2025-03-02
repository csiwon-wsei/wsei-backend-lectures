namespace architecture_problems;

public class PlateNumber
{
    private readonly Dictionary<char?, string> _provinces = new()
    {
        { 'K' ,"małopolskie"},
        { 'T', "świętokrzyskie"},
        { 'J', "małopolskie"}
    };
    public string Number { get; set; }
    public string Province => _provinces[Number?.Split(" ")[0][0]];
}