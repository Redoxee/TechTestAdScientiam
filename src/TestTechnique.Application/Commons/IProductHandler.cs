using TestTechnique.Application.Contracts;
using TestTechnique.Application.Exceptions;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Commons;

/// <summary>
/// An handler for requests on the <see cref="Product"/> repository.
/// </summary>
public interface IProductHandler
{
    /// <summary>
    /// Get all products from product repository.
    /// </summary>
    /// <returns>A list of converted <see cref="Product"/> into <see cref="ProductDto"/>.</returns>
    Task<IEnumerable<ProductDto>> GetAllAsync();
    
    /// <summary>
    /// Get a specified product from product repository.
    /// </summary>
    /// <param name="productId">The product Guid.</param>
    /// <returns>A <see cref="Product"/> converted into a <see cref="ProductDto"/>.</returns>
    /// <exception cref="ArgumentException">Throw when <see cref="Guid"/> is empty.</exception>
    /// <exception cref="EntityNotFoundException">Throw when no <see cref="Product"/> is found.</exception>
    Task<ProductDto> GetAsync(Guid productId);
    
    /// <summary>
    /// Add a product to the repository.
    /// </summary>
    /// <param name="productDto">The <see cref="ProductDto"/> to add.</param>
    /// <returns>The Guid of created <see cref="Product"/>.</returns>
    /// <exception cref="ArgumentNullException">Throw when the given <see cref="ProductDto"/> is null.</exception>
    Task<Guid> AddAsync(ProductDto productDto);
    
    /// <summary>
    /// Update the specified product from product repository.
    /// </summary>
    /// <param name="productId">The product Guid.</param>
    /// <param name="productDto">The <see cref="ProductDto"/> to update.</param>
    /// <returns>The modified product.</returns>
    /// <exception cref="ArgumentException">Throw when <see cref="Guid"/> is empty.</exception>
    /// <exception cref="ArgumentNullException">Throw when the given <see cref="ProductDto"/> is null.</exception>
    /// <exception cref="EntityNotFoundException">Throw when no <see cref="Product"/> is found.</exception>
    Task<ProductDto> UpdateAsync(ProductDto productDto);
    
    /// <summary>
    /// Delete the specified product from product repository.
    /// </summary>
    /// <param name="productId">The product Guid.</param>
    /// <exception cref="ArgumentException">Throw when <see cref="Guid"/> is empty.</exception>
    /// <exception cref="EntityNotFoundException">Throw when no <see cref="Product"/> is found.</exception>
    Task DeleteAsync(Guid productId);
}