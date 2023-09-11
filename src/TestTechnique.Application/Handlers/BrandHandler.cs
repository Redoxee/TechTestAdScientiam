using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Repositories;
using TestTechnique.Application.Extensions;
using TestTechnique.Application.Exceptions;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Handlers;

public class BrandHandler : IBrandHandler
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IBrandRepository _brandRepository;

	public BrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
	{
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		_brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
	}

	public async Task<IEnumerable<BrandDto>> GetAllAsync()
	{
		var allBrands = await _brandRepository.GetAllAsync();
		return allBrands.Select(brand => brand.From());
	}

	public async Task<BrandDto> GetAsync(Guid id)
	{
		var brand = await _brandRepository.GetAsync(id);
		if (brand == null)
		{
			throw new EntityNotFoundException();
		}

		return brand.From();
	}

	public async Task<Guid> AddAsync(BrandDto brandDto)
	{
		if (brandDto == null)
		{
			throw new ArgumentNullException($"Argument: {nameof(brandDto)} is null");
		}

		var alreadyPresentBrand = await _brandRepository.GetAsync(brandDto.Id);
		if (alreadyPresentBrand != null)
		{
			throw new EntityAlreadyExistException($"Brand already exist Id:{brandDto.Id}");
		}

		var result = await _brandRepository.AddAsync(brandDto.To());
		await _unitOfWork.SaveChangesAsync();

		return result;
	}

	public async Task<BrandDto> UpdateAsync(BrandDto brandDto)
	{
		if (brandDto == null)
		{
			throw new ArgumentNullException($"Argument: {nameof(brandDto)} is null");
		}

		var brand = await _brandRepository.GetAsync(brandDto.Id);
		if (brand == null)
		{
			throw new EntityNotFoundException($"Brand not found Id:{brandDto.Id}");
		}

		await _brandRepository.UpdateAsync(brand);
		await _unitOfWork.SaveChangesAsync();
		return brand.From();
	}

	public async Task DeleteAsync(Guid id)
	{
		var brand = await _brandRepository.GetAsync(id);
		if (brand == null)
		{
			throw new EntityNotFoundException($"Brand not found Id:{id}");
		}

		await _brandRepository.DeleteAsync(brand);
		await _unitOfWork.SaveChangesAsync();
		return;
	}
}
