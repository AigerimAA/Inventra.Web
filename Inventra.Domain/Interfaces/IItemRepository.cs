using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Item>> GetByInventoryIdAsync(int inventoryId, CancellationToken cancellationToken = default);
        Task AddAsync(Item item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
        Task SetOriginalVersionAsync(Item item, byte[] expectedVersion, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
