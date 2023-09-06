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
            throw new EntityNotFoundException($"Product not fond Id:{id}");
        }

        return product.From();
    }

    public Task<Guid> AddAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> UpdateAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}