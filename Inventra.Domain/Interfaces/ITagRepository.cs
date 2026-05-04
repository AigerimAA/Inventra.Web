using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetByPrefixAsync(string prefix, int maxResults = 10);
        Task<Tag?> GetByNameAsync(string name);
        Task AddAsync(Tag tag);
        void RemoveInventoryTags(IList<InventoryTag> inventoryTags);
        Task<Tag> GetOrCreateAsync(string name);
        Task<IEnumerable<(string Name, int Count)>> GetTagsWithCountAsync(int maxTags = 50);
    }
}
