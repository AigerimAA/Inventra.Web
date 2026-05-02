namespace Inventra.Application.Interfaces
{
    public interface ICustomIdGenerator
    {
        Task<string> GenerateAsync(int inventoryId, CancellationToken cancellationToken = default);
    }
}
