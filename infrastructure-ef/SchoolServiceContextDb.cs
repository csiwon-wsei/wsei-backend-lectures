using core.Models;
using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef;

public class SchoolServiceContextDb: DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroup> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Room>()
            .HasKey(c => new { c.Floor, c.RoomKey, c.HotelId });
        modelBuilder.Entity<Room>()
            .HasBaseType<BasicEntity>();
    }
}
