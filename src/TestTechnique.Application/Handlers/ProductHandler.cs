using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Repositories;

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

    public Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
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