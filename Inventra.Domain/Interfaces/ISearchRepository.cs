using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Inventory>> SearchInventoriesAsync(string query);
        Task<IEnumerable<Item>> SearchItemsAsync(string query);
    }
}
