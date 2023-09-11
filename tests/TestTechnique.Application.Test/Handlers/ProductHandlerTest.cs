using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

using TestTechnique.Application.Handlers;
using TestTechnique.Application.Commons;
using TestTechnique.Application.Repositories;
using TestTechnique.Application.Exceptions;
using TestTechnique.Domain.Models;
using System;
using TestTechnique.Application.Contracts;

namespace TestTechnique.Application.Test.Handlers;

public class ProductHandlerTest
{
	private ProductHandler _productHandler;
	private Mock<IProductRepository> _productRepository;

	public ProductHandlerTest()
	{
		var unitOfWork = new Mock<IUnitOfWork>();
		_productRepository = new Mock<IProductRepository>();
		var brandRepository = new Mock<IBrandRepository>();
		_productHandler = new ProductHandler(unitOfWork.Object, _productRepository.Object, brandRepository.Object);
	}

	[Fact]
	public async Task Get_All()
	{
		// Arrange
		_productRepository.Setup(x => x.GetAllAsync())
			.ReturnsAsync(new List<Product>());

		// Act
		var response = await _productHandler.GetAllAsync();

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task Get_One()
	{
		// Arrange
		_productRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
			.ReturnsAsync(new Product());

		// Act
		var response = await _productHandler.GetAsync(new Guid());

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task Get_One_NotFound()
	{
		// Arrange
		Task<Product> task = new Task<Product>(() => null);
		task.Start();
		_productRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
			.Returns(task);

		// Act
		try
		{
			var response = await _productHandler.GetAsync(new Guid());
		}
		catch(EntityNotFoundException ex)
		{
			// Assert
			Assert.NotNull(ex);
		}
		catch (Exception ex)
		{
			Assert.Fail($"Unexpected exception {ex.GetType()}");
		}
	}

	[Fact]
	public async Task Add()
	{
		// Arrange
		Guid guid = Guid.NewGuid();
		_productRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
			.ReturnsAsync(guid);
		var productDto = new ProductDto();
		productDto.Brand = string.Empty;

		// Act
		var response = await _productHandler.AddAsync(productDto);

		// Assert
		Assert.Equal(guid, response);
	}

	[Fact]
	public async Task Add_null_brand()
	{
		try
		{
			// Act
			await _productHandler.AddAsync(new ProductDto());
		}
		catch(ArgumentException ex)
		{
			// Assert
			Assert.NotNull(ex);
		}
		catch (Exception ex)
		{
			Assert.Fail($"Unexpected exception {ex.GetType()}");
		}
	}

	[Fact]
	public async Task Update()
	{
		// Arrange
		ProductDto productDto = new ProductDto();
		productDto.Brand = string.Empty;

		_productRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
			.ReturnsAsync(new Product());

		Task mockTask  = new Task<Product>(()=>new Product());
		mockTask.Start();
		_productRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
			.Returns(mockTask);

		// Act
		var response = await _productHandler.UpdateAsync(productDto);

		// Assert
		Assert.NotNull(response);
	}

	[Fact]
	public async Task Update_NotFound()
	{
		// Arrange
		ProductDto productDto = new ProductDto();
		productDto.Brand = string.Empty;

		Task<Product> task = new Task<Product>(() => null);
		task.Start();
		_productRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
			.Returns(task);

		// Act
		try
		{
			var response = await _productHandler.UpdateAsync(productDto);
		}
		catch (EntityNotFoundException ex)
		{
			// Assert
			Assert.NotNull(ex);
		}
		catch (Exception ex)
		{
			Assert.Fail($"Unexpected exception {ex.GetType()}");
		}
	}

	[Fact]
	public async Task Delete()
	{
		_productRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
			.ReturnsAsync(new Product());

		await _productHandler.DeleteAsync(new Guid());
	}

	[Fact]
	public async Task Delete_NotFound()
	{
		try
		{
			await _productHandler.DeleteAsync(new Guid());
		}
		catch (EntityNotFoundException ex)
		{
			// Assert
			Assert.NotNull(ex);
		}
		catch (Exception ex)
		{
			Assert.Fail($"Unexpected exception {ex.GetType()}");
		}
	}
}
