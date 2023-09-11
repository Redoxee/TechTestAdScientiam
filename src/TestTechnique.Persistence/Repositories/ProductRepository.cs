using Microsoft.EntityFrameworkCore;
using TestTechnique.Application.Exceptions;
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
            try
            {
                if (asTracking)
                {
                    return _dbContext.Products.AsTracking().Include(product => product.Brand).Single((product) => product.Id == id);
                }
                else
                {
                    return _dbContext.Products.AsNoTracking().Include(product => product.Brand).Single((product) => product.Id == id);
                }

            }
            catch (InvalidOperationException ex)
            {
                // TODO : Remove log or convert it to Logger.
                Console.WriteLine($"Entity not found Id:{id}", ex);
                return null;
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
            _dbContext.Products.Add(entity);
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
			_dbContext.Products.AddRange(entities);
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
			_dbContext.Products.Update(entity);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task UpdateAsync(IEnumerable<Product> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Products.UpdateRange(entities);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task DeleteAsync(Product entity)
	{
		var task = new Task(() =>
		{
			_dbContext.Products.Remove(entity);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task DeleteAsync(IEnumerable<Product> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Products.RemoveRange(entities);
		});

		task.Start();
		return task;
	}

    /// <inheritdoc />
    public Task<Product> GetByNameAsync(string name)
	{
        var task = Task.Run<Product>(() =>
        {
            try
            {
                return _dbContext.Products.AsNoTracking().Include(p => p.Brand).Single((product) => product.Name == name);
			}
			catch (InvalidOperationException)
			{
				return null;
			}
		});

		return task;
	}
}