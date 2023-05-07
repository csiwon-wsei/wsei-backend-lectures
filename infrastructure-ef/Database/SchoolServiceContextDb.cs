using core.Domain;
using infrastructure_ef.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef;
/// <summary>
/// Przykładowa klasa kontekstu.
/// Encjami są klasy zdefiniowane w module infrastuktury.
/// </summary>
public class SchoolServiceContextDb : DbContext
{
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<StudentGroupEntity> Groups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            //.UseLazyLoadingProxies()                    // włączenie ładowania leniwego
            .UseInMemoryDatabase("SchoolAppInfrastructure");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
            .HasKey(c => new {c.Floor, c.RoomKey, c.HotelId});

        modelBuilder.Entity<StudentEntity>()
            .HasKey(s => s.Id);
        
        // Ukryta właściwość - data utworzenia obiektu.
        modelBuilder.Entity<StudentEntity>()
            .Property<DateTime?>("Created")
            .IsRequired(false);

        modelBuilder.Entity<StudentEntity>()
            .OwnsOne<Address>(s => s.ContactAddress);
        
        modelBuilder.Entity<StudentEntity>()
            .HasOne<StudentGroupEntity>(g => g.StudentGroupEntity)
            .WithMany(g => g.Students)
            .IsRequired(false);

        modelBuilder.Entity<StudentEntity>()
            .Property<string>(s => s.Phone)
            .HasMaxLength(12)
            .IsRequired(false);

        modelBuilder.Entity<StudentEntity>()
            .Property(s => s.Birth)
            .IsRequired();

        modelBuilder.Seed();
    }
}