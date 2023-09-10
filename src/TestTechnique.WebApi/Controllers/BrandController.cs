using Microsoft.AspNetCore.Mvc;
using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Exceptions;

namespace TestTechnique.WebApi.Controllers;

/// <summary>
/// Controller API to manage brands.
/// </summary>
[ApiController]
[Route("[controller]")]
internal class BrandController : ControllerBase
{
	private readonly ILogger<BrandController> _logger;
	private readonly IBrandHandler _brandHandler;

	/// <summary>
	/// Ctor
	/// </summary>
	/// <param name="logger">The logger of application.</param>
	/// <param name="brandHandler">The handler of brand request.</param>
	/// <exception cref="ArgumentNullException">Throw if a service is missing.</exception>
	public BrandController(ILogger<BrandController> logger, IBrandHandler brandHandler)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_brandHandler = brandHandler ?? throw new ArgumentNullException(nameof(logger));
	}

	/// <summary>
	/// Get all brands.
	/// </summary>
	/// <returns>A list of <see cref="BrandDto"/></returns>
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var brands = await _brandHandler.GetAllAsync();
		return Ok(brands);
	}

	/// <summary>
	/// Get a brand by ID.
	/// </summary>
	/// <param name="id">The Guid of brand.</param>
	/// <returns>The specified <see cref="BrandDto"/>.</returns>
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Get([FromRoute] Guid id)
	{
		try
		{
			var brand = await _brandHandler.GetAsync(id);
			return Ok(brand);
		}
		catch (EntityNotFoundException ex)
		{
			_logger.LogWarning($"Brand not found Id:{id}", ex);
			return NotFound();
		}
	}

	/// <summary>
	/// Create a brand.
	/// </summary>
	/// <param name="brandDto">The brand to add.</param>
	/// <returns>The route and the Guid of created <see cref="BrandDto"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Post([FromQuery] BrandDto brandDto)
	{
		// TODO : should we test if the element is already present ?
		var brandId = await _brandHandler.AddAsync(brandDto);
		_logger.LogInformation($"The {brandDto.Name} has been added with the Id:{brandDto.Id}.");
		return CreatedAtAction("Get", brandId);
	}

	/// <summary>
	/// Update a brand.
	/// </summary>
	/// <param name="id">The Guid of brand.</param>
	/// <param name="brandDto">The brand to update.</param>
	/// <returns>The brand with data updated.</returns>
	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Put([FromHeader] Guid id, [FromRoute] BrandDto brandDto)
	{
		try
		{
			await _brandHandler.UpdateAsync(brandDto);
			_logger.LogInformation($"Updated brand Id:{id}");
			// TODO : it feel weird to return the sent brandDto when we could return the one given by _brandHandler.UpdateAsync
			return Ok(brandDto);
		}
		catch (EntityNotFoundException)
		{
			return NotFound();
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error while updating brand Id:{brandDto.Id}", ex);
			return UnprocessableEntity(id);
		}
	}

	/// <summary>
	/// Delete a brand.
	/// </summary>
	/// <param name="id">The Guid of brand.</param>
	/// <returns>No content.</returns>
	[HttpDelete]
	public async Task<IActionResult> Delete([FromHeader] Guid id)
	{
		var existingBrand = await _brandHandler.GetAsync(id);
		if (existingBrand == null)
		{
			_logger.LogError($"Brand not found Id:{id}");
			return NotFound();
		}

		await _brandHandler.DeleteAsync(id);
		_logger.LogInformation($"The brand with Id: {id} has been deleted.");
		return NoContent();
	}
}
