using TestTechnique.Application.Contracts;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Extensions;

internal static class ProductDtoExtension
{
    public static ProductDto From(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            // TODO : is the right way to convert brand to string or should it be handled by ToString ? Also what to do when there's no brand ?
            Brand = product.Brand?.Name ?? "",
        };
    }

    public static Product To(this ProductDto product, Brand brand) {
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Brand = brand
        };
    }
}
