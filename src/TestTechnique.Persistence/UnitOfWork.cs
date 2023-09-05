using TestTechnique.Application.Commons;

namespace TestTechnique.Persistence;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly TestTechniqueDbContext _dbContext;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <exception cref="ArgumentNullException">Throw if a service is missing.</exception>
    public UnitOfWork(TestTechniqueDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc />
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}