using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetByInventoryIdAsync(int inventoryId)
        {
            return await _context.Comments
                .Include(c => c.Author)
                .Where(c => c.InventoryId == inventoryId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }
    }
}
