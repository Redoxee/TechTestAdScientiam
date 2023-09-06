using Microsoft.AspNetCore.Mvc;
using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;

namespace TestTechnique.WebApi.Controllers;

/// <summary>
/// Controller API to manage products.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductHandler _productHandler;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="logger">The logger of application.</param>
    /// <param name="productHandler">The handler of product request.</param>
    /// <exception cref="ArgumentNullException">Throw if a service is missing.</exception>
    public ProductController(ILogger<ProductController> logger, IProductHandler productHandler)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productHandler = productHandler ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all products.
    /// </summary>
    /// <returns>A list of <see cref="ProductDto"/></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productHandler.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get a product by ID.
    /// </summary>
    /// <param name="id">The Guid of product.</param>
    /// <returns>The specified <see cref="ProductDto"/>.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var product = await _productHandler.GetAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Create a product.
    /// </summary>
    /// <param name="productDto">The product to add.</param>
    /// <returns>The route and the Guid of created <see cref="ProductDto"/>.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] ProductDto productDto)
    {
        // await _productRepository.AddAsync(product);
        return NoContent();
        _logger.LogInformation($"The {productDto.Name} has been added with the ID:{productDto.Id}.");
    }

    /// <summary>
    /// Update a product.
    /// </summary>
    /// <param name="id">The Guid of product.</param>
    /// <param name="productDto">The product to update.</param>
    /// <returns>The product with data updated.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromHeader] Guid id, [FromRoute] ProductDto productDto)
    {
        // TODO : Should we check if id and productDto.Id match ?
        var productToUpdate = await _productHandler.GetAsync(id);
        if (productToUpdate == null)
        {
            return NotFound();
        }

        try
        {
            var updatedProduct = await _productHandler.UpdateAsync(productDto);
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating product {productDto.Id}", ex);
            return UnprocessableEntity(id);
        }
    }

    /// <summary>
    /// Delete a product.
    /// </summary>
    /// <param name="id">The Guid of product.</param>
    /// <returns>No content.</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromHeader] Guid id)
    {
        var product = new ProductDto { Id = id };
        // await _productHandler.DeleteAsync(product);
        Console.Write("The product with ID: {0} has been deleted.", id);
        return NotFound();
    }
}