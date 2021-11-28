using Microsoft.EntityFrameworkCore;
namespace Benday.ArmDemo1.Api;

public class MyDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MyDbContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public DbSet<Person> Persons { get; set; }
}
