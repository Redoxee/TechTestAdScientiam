using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Repositories;
using TestTechnique.Application.Extensions;
using TestTechnique.Application.Exceptions;

namespace TestTechnique.Application.Handlers;

public class ProductHandler : IProductHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public ProductHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
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
            throw new EntityNotFoundException($"Product not found Id:{id}");
        }

        return product.From();
    }

    public async Task<Guid> AddAsync(ProductDto productDto)
    {
        var alreadyPresentProduct = await _productRepository.GetAsync(productDto.Id);
        if(alreadyPresentProduct != null)
        {
            throw new EntityAlreadyExistException($"Product already exist Id:{productDto.Id}");
        }

        return await _productRepository.AddAsync(productDto.To());
    }

    public async Task<ProductDto> UpdateAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetAsync(productDto.Id);
        if (product == null)
        {
            throw new EntityNotFoundException($"Product not found Id:{productDto.Id}");
        }

        productDto.CopyTo(product);
        await _productRepository.UpdateAsync(product);
        return productDto;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);
        if (product == null)
        {
            throw new EntityNotFoundException($"Product not found Id:{id}");
        }

        await _productRepository.DeleteAsync(product);
        return;
    }
}