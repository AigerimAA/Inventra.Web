using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.InventoryTags)
                    .ThenInclude(it => it.Tag)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.InventoryTags)
                    .ThenInclude(it => it.Tag)
                .Include(i => i.Items)
                .ToListAsync();
        }
        public async Task<IEnumerable<Inventory>> GetByOwnerIdAsync(string ownerId)
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.InventoryTags)
                    .ThenInclude(it => it.Tag)
                .Include(i => i.Items)
                .Where(i => i.OwnerId == ownerId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Inventory>> GetLatestAsync(int count)
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.Items)
                .Include(i => i.InventoryTags)
                    .ThenInclude(it => it.Tag)
                .OrderByDescending(i => i.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
        public async Task<IEnumerable<Inventory>> GetMostPopularAsync(int count)
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Items)
                .OrderByDescending(i => i.Items.Count)
                .Take(count)
                .ToListAsync();
        }
        public async Task AddAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
        }
        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
        }
        public async Task DeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory is not null)
                _context.Inventories.Remove(inventory);
        }
        public void RemoveInventoryTags(IEnumerable<InventoryTag> inventoryTags)
        {
            _context.InventoryTags.RemoveRange(inventoryTags);
        }
    }
}
