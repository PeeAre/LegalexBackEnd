using LegalexBackEnd.Models.Order;
using Microsoft.EntityFrameworkCore;
public sealed class ApplicationContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        if (Database.GetPendingMigrations().Any())
        {
            Database.Migrate();
        }

        Database.EnsureCreated();
    }
}