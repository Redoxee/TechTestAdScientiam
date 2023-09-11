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

    public static void CopyTo(this ProductDto product, Product other)
    {
        other.Id = product.Id;
        other.Name = product.Name;
        other.Description = product.Description;
        other.Price = product.Price;
        // TODO : I'm still not sure about how to handle Brand
        other.Brand.Name = product.Brand;
        other.Brand.Id = Guid.Empty;
    }
}
