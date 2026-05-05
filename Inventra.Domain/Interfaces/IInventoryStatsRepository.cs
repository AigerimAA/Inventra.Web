using Inventra.Domain.ValueObjects;

namespace Inventra.Domain.Interfaces
{
    public interface IInventoryStatsRepository
    {
        Task<InventoryStats> GetStatsAsync(int inventoryId, CancellationToken cancellationToken);
    }
}
