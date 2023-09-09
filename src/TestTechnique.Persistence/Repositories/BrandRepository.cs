using Microsoft.EntityFrameworkCore;
using TestTechnique.Application.Repositories;
using TestTechnique.Domain.Models;

namespace TestTechnique.Persistence.Repositories;

internal class BrandRepository : IBrandRepository
{
	private readonly TestTechniqueDbContext _dbContext;

	/// <summary>
	/// Ctor
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	/// <exception cref="ArgumentNullException">Throw if a service is missing.</exception>
	public BrandRepository(TestTechniqueDbContext dbContext)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	}

	/// <inheritdoc />
	public Task<Guid> AddAsync(Brand entity)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task<IEnumerable<Guid>> AddAsync(IEnumerable<Brand> entities)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task DeleteAsync(Brand entity)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task DeleteAsync(IEnumerable<Brand> entities)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task<IEnumerable<Brand>> GetAllAsync()
	{
		var task = new Task<IEnumerable<Brand>>(() => {
			return _dbContext.Brands.AsNoTracking().ToList();
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task<Brand> GetAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task<Brand> GetAsync(Guid id, bool asTracking)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task<Brand> GetBrandByName(string name)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task UpdateAsync(Brand entity)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task UpdateAsync(IEnumerable<Brand> entities)
	{
		throw new NotImplementedException();
	}
}
