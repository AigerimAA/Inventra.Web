using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetByOwnerIdAsync(string OwnerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetLatestAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetMostPopularAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetWithAccessByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task AddAsync(Inventory inventory, CancellationToken cancellationToken = default);
        Task UpdateAsync(Inventory inventory, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        void RemoveInventoryTags(IEnumerable<InventoryTag> inventoryTags);
        void SetOriginalVersion(Inventory inventory, byte[] expectedVersion);
    }
}
