using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef.Entities;

public class Address
{
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }

    public override string ToString()
    {
        return $"{nameof(City)}: {City}, {nameof(ZipCode)}: {ZipCode}, {nameof(Street)}: {Street}";
    }
}