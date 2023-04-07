using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_api_jwt.Models;

namespace web_api_jwt.Data;

public static class SeedData
{
    public static async void CreateUsers(this IApplicationBuilder app)
    {
        int a = 10;
        using (var scope = app.ApplicationServices.CreateScope())
        {
            UserManager<IdentityUser>? manager = scope
                .ServiceProvider.GetService<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser
            {
                Id = "1Asd1aadcv",
                UserName = "karol",
                NormalizedUserName = "KAROL",
                Email = "karol@wsei.edu.pl",
                EmailConfirmed = true,
                NormalizedEmail = "KAROL@WSEI.EDU.PL",
                PhoneNumberConfirmed = true,
                PhoneNumber = "111222333",
                AccessFailedCount = 0
            };
            if (await manager.FindByNameAsync("karol") is null)
            {
                IdentityResult result = await manager.CreateAsync(user, "1234aBCD$");
                var identityErrors = result.Errors;
            }
        }
    }
    
    public static List<IdentityRole> CreateRoles()
    {
        return new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            }
        };
    }
    

    public static IEnumerable<Book> CreateBooks()
    {
        return new List<Book>()
        {
            new()
            {
                Id = 1,
                Title = "C#",
                EditionYear = 2020
            },
            new()
            {
                Id = 2,
                Title = "Java",
                EditionYear = 2022
            },
            new()
            {
                Id = 3,
                Title = "ASP.NET",
                EditionYear = 2023
            }
        };
    }

    public static void EnsureDataSeed(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            context?.Books.AddRange(CreateBooks());
            context?.Roles.AddRange(CreateRoles());
            context?.SaveChanges();
        }
    }
}