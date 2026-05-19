using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Like?> GetByUserAndItemAsync(int itemId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.ItemId == itemId && l.UserId == userId, cancellationToken);
        }
        public async Task AddAsync(Like like, CancellationToken cancellationToken = default)
        {
            await _context.Likes.AddAsync(like, cancellationToken);
        }
        public async Task RemoveAsync(Like like, CancellationToken cancellationToken = default)
        {
            _context.Likes.Remove(like);
        }
    }
}
