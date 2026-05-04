using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IAccessRepository
    {
        Task<IEnumerable<InventoryAccess>> GetUsersWithAccessAsync(int inventoryId);
        Task<InventoryAccess> AddAccessAsync(int inventoryId, string userId);
        Task RemoveAccessAsync(int inventoryId, string userId);
        Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string query);
    }
}
