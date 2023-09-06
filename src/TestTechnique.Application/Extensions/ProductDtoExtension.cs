using System.Runtime.CompilerServices;
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
                Price = product.Price,
                // TODO : is the right way to convert brand to string or should it be handled by ToString ?
                Brand = product.Brand.Name,
            };
        }

        public static Product To(this ProductDto product) {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                // TODO : there's definitely something missing in this conversion concerning the Brand
                Brand = new Brand
                {
                    Id = Guid.Empty,
                    Name = product.Brand,
                }
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
}
