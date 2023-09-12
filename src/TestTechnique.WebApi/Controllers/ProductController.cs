using Microsoft.AspNetCore.Mvc;
using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Exceptions;

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
        try
        {
            var product = await _productHandler.GetAsync(id);
            return Ok(product);
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogWarning($"Product not found Id:{id}", ex);
            return NotFound();
        }
    }

    /// <summary>
    /// Create a product.
    /// </summary>
    /// <param name="productDto">The product to add.</param>
    /// <returns>The route and the Guid of created <see cref="ProductDto"/>.</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery] ProductDto productDto)
    {
        // TODO : It look like a middleware pattern could be usefull here
        if (productDto.Brand == null)
        {
            return BadRequest("Missing Brand");
        }

        var productId = await _productHandler.AddAsync(productDto);
        _logger.LogInformation($"The {productDto.Name} has been added with the Id:{productDto.Id}.");
        return CreatedAtAction("Get", productId);
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
        // TODO : It look like a middleware pattern could be usefull here
        if (productDto.Brand == null)
        {
            return BadRequest("Missing Brand");
        }

		try
		{
            var updatedProduct = await _productHandler.UpdateAsync(productDto);
            _logger.LogInformation($"Updated product Id:{id}");
            return Ok(updatedProduct);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating product Id:{productDto.Id}", ex);
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
        try
        {
            await _productHandler.DeleteAsync(id);
            _logger.LogInformation($"The product with Id: {id} has been deleted.");
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}