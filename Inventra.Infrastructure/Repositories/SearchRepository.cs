using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;

        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> SearchInventoriesAsync(string query)
        {
            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.Items)
                .Where(i => i.Title.Contains(query) ||
                        (i.Description != null && i.Description.Contains(query)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Item>> SearchItemsAsync(string query)
        {
            return await _context.Items
                .Include(i => i.Inventory)
                .Include(i => i.CreatedBy)
                .Where(i => i.CustomId.Contains(query) || 
                        (i.CustomString1Value != null && i.CustomString1Value.Contains(query)) ||
                        (i.CustomString2Value != null && i.CustomString2Value.Contains(query)) ||
                        (i.CustomString3Value != null && i.CustomString3Value.Contains(query)))
            .ToListAsync();
        }
    }
}
