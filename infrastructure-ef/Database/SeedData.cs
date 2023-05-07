using core.Domain;
using infrastructure_ef.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef;

public static class SeedData
{
    public static void Seed(this ModelBuilder builder)
    {
        builder.Entity<StudentEntity>()
            .HasData(
                new StudentEntity
                {
                    Id = 1, FirstName = "Adam", LastName = "Kowal", Birth = DateOnly.Parse("2000-10-10"),
                    Phone = "1234567890", StudentGroupEntity = null
                },
                new StudentEntity
                {
                    Id = 2, FirstName = "Ewa", LastName = "Bonar", Birth = DateOnly.Parse("2001-10-10"),
                    Phone = "0987654321", StudentGroupEntity = null
                }
            );
    }
}
