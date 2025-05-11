using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_api_jwt.Models;

namespace web_api_jwt.Data;

public class AppDbContext:IdentityDbContext<IdentityUser>
{
    
    public DbSet<Book> Books {get; set;}

    public AppDbContext(string dbPath)
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, string dbPath) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AppDb");
    }
}