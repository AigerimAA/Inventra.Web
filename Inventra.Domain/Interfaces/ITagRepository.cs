using Inventra.Domain.Entities;
using Inventra.Domain.ValueObjects;

namespace Inventra.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetByPrefixAsync(string prefix, int maxResults = 10, CancellationToken cancellationToken = default);
        Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task AddAsync(Tag tag, CancellationToken cancellationToken = default);
        void RemoveInventoryTags(IList<InventoryTag> inventoryTags);
        Task<Tag> GetOrCreateAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<TagWithCount>> GetTagsWithCountAsync(int maxTags = 50, CancellationToken cancellationToken = default);
    }
}
