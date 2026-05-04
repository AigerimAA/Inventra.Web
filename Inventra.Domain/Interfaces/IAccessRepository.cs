using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IAccessRepository
    {
        Task<IEnumerable<InventoryAccess>> GetUsersWithAccessAsync(int inventoryId, CancellationToken cancellationToken = default);
        Task<InventoryAccess> AddAccessAsync(int inventoryId, string userId, CancellationToken cancellationToken = default);
        Task RemoveAccessAsync(int inventoryId, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserLookup>> SearchUsersAsync(string query, CancellationToken cancellationToken = default);
    }
}
