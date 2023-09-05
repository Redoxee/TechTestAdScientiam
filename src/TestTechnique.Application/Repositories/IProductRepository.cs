using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Repositories;

/// <summary>
/// The repository of <see cref="Product"/> entity.
/// </summary>
public interface IProductRepository : IEntityRepository<Product>
{
    /// <summary>
    /// Get a product by name.
    /// </summary>
    /// <param name="name"></param>
    /// <remarks>The entity is not tracked.</remarks>
    /// <returns>The specified entity.</returns>
    public Task<Product> GetByNameAsync(string name);
}