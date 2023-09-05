using TestTechnique.Application.Repositories;
using TestTechnique.Domain.Models;

namespace TestTechnique.Persistence.Repositories;

/// <inheritdoc />
public class ProductRepository : IProductRepository
{
    private readonly TestTechniqueDbContext _dbContext;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <exception cref="ArgumentNullException">Throw if a service is missing.</exception>
    public ProductRepository(TestTechniqueDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <inheritdoc />
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Product> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Product> GetAsync(Guid id, bool asTracking)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Guid> AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Guid>> AddAsync(IEnumerable<Product> entities)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(IEnumerable<Product> entities)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(IEnumerable<Product> entities)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Product> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}