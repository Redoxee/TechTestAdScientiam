namespace TestTechnique.Application.Contracts;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }
}