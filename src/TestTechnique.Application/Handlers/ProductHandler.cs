using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Repositories;
using TestTechnique.Application.Extensions;
using TestTechnique.Application.Exceptions;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Handlers;

public class ProductHandler : IProductHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
	private readonly IBrandRepository _brandRepository;

	public ProductHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IBrandRepository brandRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
		_brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
	}

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var allProduct = await _productRepository.GetAllAsync();
        return allProduct.Select(product => product.From());
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);
       if (product == null)
        {
            throw new EntityNotFoundException();
        }

        return product.From();
    }

    public async Task<Guid> AddAsync(ProductDto productDto)
    {
        if (productDto == null)
        {
            throw new ArgumentNullException($"Argument: {nameof(productDto)} is null");
		}

		if (productDto.Brand == null)
		{
			throw new ArgumentException($"Argument: {nameof(productDto.Brand)} is null");
		}

		var alreadyPresentProduct = await _productRepository.GetAsync(productDto.Id);
        if(alreadyPresentProduct != null)
        {
            throw new EntityAlreadyExistException($"Product already exist Id:{productDto.Id}");
        }

        Brand brand = await _brandRepository.GetBrandByName(productDto.Brand);
        var result = await _productRepository.AddAsync(productDto.To(brand));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task<ProductDto> UpdateAsync(ProductDto productDto)
	{
		if (productDto == null)
		{
			throw new ArgumentNullException($"Argument: {nameof(productDto)} is null");
		}

        if (productDto.Brand == null)
        {
            throw new ArgumentException($"Argument: {nameof(productDto.Brand)} is null");
        }

		var product = await _productRepository.GetAsync(productDto.Id);
        if (product == null)
        {
            throw new EntityNotFoundException($"Product not found Id:{productDto.Id}");
        }

		Brand brand = await _brandRepository.GetBrandByName(productDto.Brand);
		product = productDto.To(brand);
        await _productRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return product.From();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException($"Product not found Id:{id}");
        }

        await _productRepository.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return;
    }
}