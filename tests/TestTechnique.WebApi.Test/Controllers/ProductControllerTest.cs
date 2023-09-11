using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TestTechnique.Application.Commons;
using TestTechnique.Application.Contracts;
using TestTechnique.Application.Exceptions;
using TestTechnique.WebApi.Controllers;
using Xunit;

namespace TestTechnique.WebApi.Test.Controllers;

public class ProductControllerTest
{
    private readonly ProductController _productController;
    private readonly Mock<IProductHandler> _productHandler;

    public ProductControllerTest()
    {
        _productHandler = new Mock<IProductHandler>();
        _productController = new ProductController(new NullLogger<ProductController>(), _productHandler.Object);
    }

    [Fact]
    public async Task Get_Many()
    {
        // Arrange
        _productHandler
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<ProductDto>());

        // Act
        var response = await _productController.Get();

        // Assert
        Assert.NotNull(response);
        var content = Assert.IsAssignableFrom<OkObjectResult>(response);
        Assert.NotNull(content.Value);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(content.Value);
    }

    [Fact]
    public async Task Get_One()
    {
        // Arrange
        _productHandler
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new ProductDto());

        // Act
        var response = await _productController.Get(Guid.NewGuid());

        // Assert
        Assert.NotNull(response);
        var content = Assert.IsAssignableFrom<OkObjectResult>(response);
        Assert.NotNull(content.Value);
        Assert.IsType<ProductDto>(content.Value);
    }

    [Fact]
    public async Task Get_One_NotFound()
    {
        // Arrange
        _productHandler
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new EntityNotFoundException());

        // Act
        var response = await _productController.Get(Guid.NewGuid());

        // Assert
        Assert.NotNull(response);
        Assert.IsAssignableFrom<NotFoundResult>(response);
    }

    [Fact]
    public async Task Post()
    {
        // Arrange
        _productHandler
            .Setup(x => x.AddAsync(It.IsAny<ProductDto>()))
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var response = await _productController.Post(new ProductDto());

        // Assert
        Assert.NotNull(response);
        var content = Assert.IsAssignableFrom<CreatedAtActionResult>(response);
        Assert.NotNull(content.Value);
        Assert.Equal("Get", content.ActionName);
    }

    [Fact]
    public async Task Put()
    {
        // Arrange
        _productHandler
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new ProductDto());
        _productHandler
            .Setup(x => x.UpdateAsync(It.IsAny<ProductDto>()))
            .ReturnsAsync(new ProductDto());

        // Act
        var response = await _productController.Put(Guid.NewGuid(), new ProductDto());

        // Assert
        Assert.NotNull(response);
        var content = Assert.IsAssignableFrom<OkObjectResult>(response);
        Assert.NotNull(content.Value);
        Assert.IsAssignableFrom<ProductDto>(content.Value);
    }

    [Fact]
    public async Task Put_NotFound()
    {
        // Arrange
        _productHandler
            .Setup(x => x.UpdateAsync(It.IsAny<ProductDto>()))
			.ThrowsAsync(new EntityNotFoundException());

        // Act
        var response = await _productController.Put(Guid.NewGuid(), new ProductDto());

        // Assert
        Assert.NotNull(response);
        Assert.IsAssignableFrom<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange
        _productHandler
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new ProductDto());
        _productHandler
            .Setup(x => x.DeleteAsync(It.IsAny<Guid>()));

        // Act
        var response = await _productController.Delete(Guid.NewGuid());

        // Assert
        Assert.NotNull(response);
        Assert.IsAssignableFrom<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_NotFound()
    {
        // Arrange
		_productHandler
            .Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
			.ThrowsAsync(new EntityNotFoundException());

        // Act
        var response = await _productController.Delete(Guid.NewGuid());

        // Assert
        Assert.NotNull(response);
        Assert.IsAssignableFrom<NotFoundResult>(response);
    }
}