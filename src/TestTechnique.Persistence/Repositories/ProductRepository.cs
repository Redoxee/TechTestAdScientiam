using Microsoft.EntityFrameworkCore;
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
        var task = new Task<IEnumerable<Product>>(() => {
            return _dbContext.Products.AsNoTracking().Include(p => p.Brand).ToList();
        });

        task.Start();
        return task;
    }

    /// <inheritdoc />
    public Task<Product> GetAsync(Guid id)
    {
        return GetAsync(id, false);
    }

    /// <inheritdoc />
    public Task<Product> GetAsync(Guid id, bool asTracking)
    {
        var task = new Task<Product>(() => {
            if (asTracking)
            {
                return _dbContext.Products.AsTracking().Include(product => product.Brand).Single((product) => product.Id == id);
            }
            else
            {
				return _dbContext.Products.AsNoTracking().Include(product => product.Brand).Single((product) => product.Id == id);
			}
        });
        
        task.Start();
        return task;
    }

    /// <inheritdoc />
    public Task<Guid> AddAsync(Product entity)
    {
        var task = new Task<Guid>(() =>
        {
            _dbContext.Add(entity);
            return entity.Id;
        });

        task.Start();
        return task;
    }

    /// <inheritdoc />
    public Task<IEnumerable<Guid>> AddAsync(IEnumerable<Product> entities)
	{
		var task = new Task<IEnumerable<Guid>>(() =>
		{
			_dbContext.Add(entities);
			return entities.Select(entity => entity.Id);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task UpdateAsync(Product entity)
	{
		var task = new Task(() =>
		{
			_dbContext.Update(entity);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task UpdateAsync(IEnumerable<Product> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Update(entities);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task DeleteAsync(Product entity)
	{
		var task = new Task(() =>
		{
			_dbContext.Remove(entity);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task DeleteAsync(IEnumerable<Product> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Remove(entities);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task<Product> GetByNameAsync(string name)
	{
		var task = new Task<Product>(() => {
			return _dbContext.Products.AsNoTracking().Include(p => p.Brand).Single((product) => product.Name == name);
		});

		task.Start();
		return task;
	}
}