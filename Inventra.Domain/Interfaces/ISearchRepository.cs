using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Inventory>> SearchInventoriesAsync(string query, CancellationToken cancellationToken = default);
        Task<IEnumerable<Item>> SearchItemsAsync(string query, CancellationToken cancellationToken = default);
    }
}
