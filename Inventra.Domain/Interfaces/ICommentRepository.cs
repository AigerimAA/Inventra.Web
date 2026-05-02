using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByInventoryIdAsync(int inventoryId);
        Task AddAsync(Comment comment);
    }
}
