using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Repositories;

/// <summary>
/// The repository of <see cref="Brand"/> entity.
/// </summary>
public interface IBrandRepository : IEntityRepository<Brand>
{
	/// <summary>
	/// Get a brand by name.
	/// </summary>
	/// <param name="name"></param>
	/// <remarks>The entity is not tracked.</remarks>
	/// <returns>The specified entity.</returns>
	public Task<Brand> GetBrandByName(string name);
}
