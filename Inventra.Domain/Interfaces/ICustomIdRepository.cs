using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ICustomIdRepository
    {
        Task<CustomIdFormat?> GetByInventoryIdAsync(int inventoryId, CancellationToken cancellationToken = default);
        Task SaveAsync(CustomIdFormat format, CancellationToken cancellationToken = default);
    }
}
