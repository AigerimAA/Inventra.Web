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
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Inventory>();

            return await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.Items)
                .Where(i => EF.Functions.FreeText(i.Title, query)
                         || (i.Description != null
                             && EF.Functions.FreeText(i.Description, query)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Item>> SearchItemsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Item>();

            return await _context.Items
                .Include(i => i.Inventory)
                .Include(i => i.CreatedBy)
                .Where(i =>
                    EF.Functions.FreeText(i.CustomId, query) ||
                    (i.CustomString1Value != null &&
                        EF.Functions.FreeText(i.CustomString1Value, query)) ||
                    (i.CustomString2Value != null &&
                        EF.Functions.FreeText(i.CustomString2Value, query)) ||
                    (i.CustomString3Value != null &&
                        EF.Functions.FreeText(i.CustomString3Value, query)) ||
                    (i.CustomText1Value != null &&
                        EF.Functions.FreeText(i.CustomText1Value, query)) ||
                    (i.CustomText2Value != null &&
                        EF.Functions.FreeText(i.CustomText2Value, query)) ||
                    (i.CustomText3Value != null &&
                        EF.Functions.FreeText(i.CustomText3Value, query)))
                .ToListAsync();
        }
    }
}
