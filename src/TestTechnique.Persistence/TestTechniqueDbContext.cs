using Microsoft.EntityFrameworkCore;
using TestTechnique.Domain.Models;

namespace TestTechnique.Persistence;

public sealed class TestTechniqueDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

    public TestTechniqueDbContext()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public TestTechniqueDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Is only false when used with the EFCore CLI.
        // Used only for made a migration. The connection string is useless in production.
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");
        }
    }
}