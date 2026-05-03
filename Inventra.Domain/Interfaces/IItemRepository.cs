using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int id);
        Task<IEnumerable<Item>> GetByInventoryIdAsync(int inventoryId);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task SetOriginalVersionAsync(Item item, byte[] expectedVersion);
        Task DeleteAsync(int id);
    }
}
