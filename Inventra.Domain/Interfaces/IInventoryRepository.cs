using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(int id);
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<IEnumerable<Inventory>> GetByOwnerIdAsync(string OwnerId);
        Task<IEnumerable<Inventory>> GetLatestAsync(int count);
        Task<IEnumerable<Inventory>> GetMostPopularAsync(int count);
        Task<IEnumerable<Inventory>> GetWithAccessByUserIdAsync(string userId);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(int id);
        void RemoveInventoryTags(IEnumerable<InventoryTag> inventoryTags);
        void SetOriginalVersion(Inventory inventory, byte[] expectedVersion);
    }
}
