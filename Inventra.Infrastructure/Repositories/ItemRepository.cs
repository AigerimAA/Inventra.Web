using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .Include(i => i.CreatedBy)
                .Include(i => i.Likes)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Item>> GetByInventoryIdAsync(int inventoryId)
        {
            return await _context.Items
                .Include(i => i.CreatedBy)
                .Include(i => i.Likes)
                .Where(i => i.InventoryId == inventoryId)
                .ToListAsync();
        }
        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }
        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
        }
        public async Task DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item is not null)
                _context.Items.Remove(item);
        }
    }
}
