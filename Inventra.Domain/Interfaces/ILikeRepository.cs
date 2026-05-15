using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ILikeRepository
    {
        Task<Like?> GetByUserAndItemAsync(int itemId, string userId, CancellationToken cancellationToken = default);
        Task AddAsync(Like like, CancellationToken cancellationToken = default);
        Task RemoveAsync(Like like, CancellationToken cancellationToken = default);
    }
}
