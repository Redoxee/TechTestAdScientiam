using TestTechnique.Application.Contracts;
using TestTechnique.Domain.Models;

namespace TestTechnique.Application.Extensions;

internal static class BrandDtoExtension
{
	public static BrandDto From(this Brand brand)
	{
		return new BrandDto { Id = brand.Id, Name = brand.Name };
	}

	public static Brand To(this BrandDto brand)
	{
		return new Brand { Id = brand.Id, Name = brand.Name};
	}
}
