using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetByPrefixAsync(string prefix, int maxResults = 10);
        Task<Tag?> GetByNameAsync(string name);
        Task AddAsync(Tag tag);
    }
}
