using System.ComponentModel.DataAnnotations;

namespace TestTechnique.Domain.Models;

public class Brand : IEquatable<Brand>
{
    [Key] public Guid Id { get; set; }
    [MaxLength(255)] public string Name { get; set; }

    public bool Equals(Brand? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Brand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}