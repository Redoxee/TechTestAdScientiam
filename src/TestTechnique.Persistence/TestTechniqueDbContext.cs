using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
        var guidConverter = new ValueConverter<Guid, string>(
            guid => $"\"{guid.ToString()}\"",
            str => Guid.Parse(str.Replace("\"",string.Empty)));

        modelBuilder.Entity<Product>().Property((product) => product.Id).HasConversion(guidConverter);
        modelBuilder.Entity<Brand>().Property((brand)=> brand.Id).HasConversion(guidConverter);
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