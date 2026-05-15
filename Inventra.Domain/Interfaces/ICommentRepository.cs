using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByInventoryIdAsync(int inventoryId, CancellationToken cancellationToken = default);
        Task AddAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}
