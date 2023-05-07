namespace core.Domain;

public class StudentPayments
{
    public int StudentId { get; set; }
    public string AccountNumber { get; set; } 
    public List<decimal> Payments { get; set; }
}