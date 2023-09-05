namespace TestTechnique.Application.Commons;

/// <summary>
/// Service to manage databases context.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Save all databases context.
    /// </summary>
    void SaveChanges();
    
    /// <summary>
    /// Save all databases context.
    /// </summary>
    Task SaveChangesAsync();
}