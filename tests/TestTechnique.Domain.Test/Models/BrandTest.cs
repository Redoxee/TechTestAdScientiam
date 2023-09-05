using TestTechnique.Domain.Models;
using Xunit;

namespace TestTechnique.Domain.Test.Models;

public class BrandTest
{
    [Fact]
    public void Brand_Are_Equals()
    {
        // Arrange
        var productA = new Brand { Name = "hWLpaHV" };
        var productB = new Brand { Name = "hWLpaHV" };

        // Act
        var result = productA.Equals(productB);

        // Assert
        Assert.True(result);
        Assert.Equal(productA, productB);
    }
}