using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface ILikeRepository
    {
        Task<Like?> GetByUserAndItemAsync(int itemId, string userId);
        Task AddAsync(Like like);
        Task RemoveAsync(Like like);
    }
}
