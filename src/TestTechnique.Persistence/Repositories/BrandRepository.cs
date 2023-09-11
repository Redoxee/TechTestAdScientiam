using Microsoft.EntityFrameworkCore;
using TestTechnique.Application.Exceptions;
using TestTechnique.Application.Repositories;
using TestTechnique.Domain.Models;

namespace TestTechnique.Persistence.Repositories;

public class BrandRepository : IBrandRepository
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
	public Task<IEnumerable<Brand>> GetAllAsync()
	{
		var task = new Task<IEnumerable<Brand>>(() => {
			return _dbContext.Brands.AsNoTracking().ToList();
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task<Brand> GetAsync(Guid id, bool asTracking)
	{
		var task = new Task<Brand>(() => {
			try
			{
				if (asTracking)
				{
					return _dbContext.Brands.AsTracking().Single((brand) => brand.Id == id);
				}
				else
				{
					return _dbContext.Brands.AsNoTracking().Single((brand) => brand.Id == id);
				}

			}
			catch (InvalidOperationException ex)
			{
				throw new EntityNotFoundException($"Entity not found Id:{id}", ex);
			}
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task<Brand> GetAsync(Guid id)
	{
		return GetAsync(id, false);
	}

	/// <inheritdoc />
	public Task<Guid> AddAsync(Brand entity)
	{
		var task = new Task<Guid>(() =>
		{
			_dbContext.Brands.Add(entity);
			return entity.Id;
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task<IEnumerable<Guid>> AddAsync(IEnumerable<Brand> entities)
	{
		var task = new Task<IEnumerable<Guid>>(() =>
		{
			_dbContext.Brands.AddRange(entities);
			return entities.Select(entity => entity.Id);
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task UpdateAsync(Brand entity)
	{
		var task = new Task(() =>
		{
			_dbContext.Brands.Update(entity);
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task UpdateAsync(IEnumerable<Brand> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Brands.UpdateRange(entities);
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task DeleteAsync(Brand entity)
	{
		var task = new Task(() =>
		{
			_dbContext.Brands.Remove(entity);
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task DeleteAsync(IEnumerable<Brand> entities)
	{
		var task = new Task(() =>
		{
			_dbContext.Brands.RemoveRange(entities);
		});

		task.Start();
		return task;
	}

	/// <inheritdoc />
	public Task<Brand> GetBrandByName(string name)
	{
		var task = Task.Run<Brand>(() =>
		{
			try
			{
				return _dbContext.Brands.AsNoTracking().Single((brand) => brand.Name == name);
			}
			catch (InvalidOperationException)
			{
				return null;
			}
		});

		return task;
	}
}
