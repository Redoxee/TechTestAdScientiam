using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestTechnique.Domain.Models;
using TestTechnique.Persistence.Repositories;
using Xunit;

namespace TestTechnique.Persistence.Test.Repositories;

public class ProductRepositoryTest
{
	public ProductRepositoryTest()
	{
		// probably better to do https://stackoverflow.com/questions/54219742/mocking-ef-core-dbcontext-and-dbset
	}

	[Fact]
	public async Task Get_Many()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);
			await dbContext.Products.AddAsync(new Product { Id = Guid.NewGuid(), Brand = brand, Description = "Description", Name = "Product_1" });
			await dbContext.Products.AddAsync(new Product { Id = Guid.NewGuid(), Brand = brand, Description = "Description", Name = "Product_2" });
			await dbContext.Products.AddAsync(new Product { Id = Guid.NewGuid(), Brand = brand, Description = "Description", Name = "Product_3" });
			await dbContext.SaveChangesAsync();

			ProductRepository productRepository = new ProductRepository(dbContext);
			var result = await productRepository.GetAllAsync();

			Assert.NotNull(result);
			Assert.Equal(3, result.Count());
		}
	}

	[Fact]
	public async Task Get_One()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);
			Guid productId = Guid.NewGuid();
			await dbContext.Products.AddAsync(new Product { Id = productId, Brand = brand, Description = "Description", Name = "Product_1" });
			await dbContext.SaveChangesAsync();

			ProductRepository productRepository = new ProductRepository(dbContext);
			var result = await productRepository.GetAsync(productId);

			Assert.NotNull(result);
			Assert.Equal(productId, result.Id);
		}
	}

	[Fact]
	public async Task Get_One_NotFound()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);
			await dbContext.Products.AddAsync(new Product { Id = Guid.NewGuid(), Brand = brand, Description = "Description", Name = "Product_1" });
			await dbContext.SaveChangesAsync();

			Guid productId = Guid.NewGuid();
			ProductRepository productRepository = new ProductRepository(dbContext);
			var result = await productRepository.GetAsync(productId);

			Assert.Null(result);
		}
	}

	[Fact]
	public async Task Add()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);


			Guid productId = Guid.NewGuid();
			ProductRepository productRepository = new ProductRepository(dbContext);
			Product product = new Product
			{
				Id = productId,
				Brand = brand,
				Name = "Added Product",
				Description = "Description",
				Price = 2
			};

			var result = await productRepository.AddAsync(product);
			await dbContext.SaveChangesAsync();

			Assert.Equal(productId, result);
		}
	}

	[Fact]
	public async Task Put()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);
			Guid productId = Guid.NewGuid();
			await dbContext.Products.AddAsync(new Product { Id = productId, Brand = brand, Description = "Description", Name = "Product_1" });
			await dbContext.SaveChangesAsync();

			ProductRepository productRepository = new ProductRepository(dbContext);

			Product modifiedProduct = new Product
			{
				Id = productId,
				Name = "Modified Name",
				Price = 1,
				Description = "Description",
				Brand = brand
			};

			await productRepository.UpdateAsync(modifiedProduct);
			await dbContext.SaveChangesAsync();

			var result = await dbContext.Products.SingleAsync((p) => p.Id == productId);

			Assert.NotNull(result);
			Assert.Equal(productId, result.Id);
			Assert.Equal("Modified Name", result.Name);
		}
	}

	[Fact]
	public async Task Delete()
	{
		using (var dbContext = GetDbContext())
		{
			Brand brand = new Brand { Id = Guid.NewGuid(), Name = "Brand_1" };
			await dbContext.Brands.AddAsync(brand);
			Guid productId = Guid.NewGuid();
			Product product = new Product { Id = productId, Brand = brand, Description = "Description", Name = "Product_1" };
			await dbContext.Products.AddAsync(product);
			dbContext.SaveChanges();

			ProductRepository productRepository = new ProductRepository(dbContext);
			await productRepository.DeleteAsync(product);
			await dbContext.SaveChangesAsync();

			Assert.Equal(0, dbContext.Products.Count());
		}
	}

	private TestTechniqueDbContext GetDbContext()
	{
		DbContextOptions<TestTechniqueDbContext> dbOptions = new DbContextOptionsBuilder<TestTechniqueDbContext>()
			.UseInMemoryDatabase("TestTechnique")
			.EnableSensitiveDataLogging()
			.Options;
		var dbContext = new TestTechniqueDbContext(dbOptions);
		dbContext.Database.EnsureDeleted();
		return dbContext;
	}
}