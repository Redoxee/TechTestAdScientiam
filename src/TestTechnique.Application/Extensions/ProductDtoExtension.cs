using TestTechnique.Application.Contracts;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Extensions
{
    internal static class ProductDtoExtension
    {
        // TODO : is it the right place to declare this relation ?
        public static ProductDto From(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                // TODO : is the right way to convert brand to string or should it be handled by ToString ?
                Brand = product.Brand.Name,
                Price = product.Price,
            };
        }
    }
}
