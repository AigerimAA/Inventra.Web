using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Options;
using Inventra.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventra.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;
        private readonly bool _useFullText;
        private readonly ILogger<SearchRepository> _logger;

        public SearchRepository(AppDbContext context, IOptions<SearchOptions> options, ILogger<SearchRepository> logger)
        {
            _context = context;
            _useFullText = options.Value.UseFullText;
            _logger = logger;
        }

        public async Task<IEnumerable<Inventory>> SearchInventoriesAsync(string query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Inventory>();

            var queryable = _context.Inventories.AsNoTracking();
            var pattern = $"%{query}%";

            IQueryable<Inventory> filtered;

            if (_useFullText)
            {
                try
                {
                    filtered = queryable.Where(i =>
                        EF.Functions.FreeText(i.Title, query) ||
                        (i.Description != null && EF.Functions.FreeText(i.Description, query)) ||
                        i.InventoryTags.Any(t => t.Tag.Name == query)
                    );
                }
                catch
                {
                    filtered = queryable.Where(i =>
                        EF.Functions.Like(i.Title, pattern) ||
                        (i.Description != null && EF.Functions.Like(i.Description, pattern)) ||
                        i.InventoryTags.Any(t => EF.Functions.Like(t.Tag.Name, pattern))
                    );
                }
            }
            else
            {
                filtered = queryable.Where(i =>
                    EF.Functions.Like(i.Title, pattern) ||
                    (i.Description != null && EF.Functions.Like(i.Description, pattern)) ||
                    i.InventoryTags.Any(t => EF.Functions.Like(t.Tag.Name, pattern))
                );
            }

            return await filtered
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.Items)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Item>();

            var baseQuery = _context.Items
                .AsNoTracking()
                .Include(i => i.Inventory)
                .Include(i => i.CreatedBy);

            if (_useFullText)
            {
                try
                {
                    return await baseQuery
                        .Where(i =>
                            EF.Functions.FreeText(i.CustomId, query) ||
                            (i.CustomString1Value != null && EF.Functions.FreeText(i.CustomString1Value, query)) ||
                            (i.CustomString2Value != null && EF.Functions.FreeText(i.CustomString2Value, query)) ||
                            (i.CustomString3Value != null && EF.Functions.FreeText(i.CustomString3Value, query)) ||
                            (i.CustomText1Value != null && EF.Functions.FreeText(i.CustomText1Value, query)) ||
                            (i.CustomText2Value != null && EF.Functions.FreeText(i.CustomText2Value, query)) ||
                            (i.CustomText3Value != null && EF.Functions.FreeText(i.CustomText3Value, query)))
                        .ToListAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    _logger.LogWarning(ex, "FullText search failed. Falling back to LIKE");
                }
            }

            var pattern = $"%{query}%";
            return await baseQuery
                .Where(i =>
                    EF.Functions.Like(i.CustomId, pattern) ||
                    (i.CustomString1Value != null && EF.Functions.Like(i.CustomString1Value, pattern)) ||
                    (i.CustomString2Value != null && EF.Functions.Like(i.CustomString2Value, pattern)) ||
                    (i.CustomString3Value != null && EF.Functions.Like(i.CustomString3Value, pattern)) ||
                    (i.CustomText1Value != null && EF.Functions.Like(i.CustomText1Value, pattern)) ||
                    (i.CustomText2Value != null && EF.Functions.Like(i.CustomText2Value, pattern)) ||
                    (i.CustomText3Value != null && EF.Functions.Like(i.CustomText3Value, pattern)))
                .ToListAsync(cancellationToken);
        }
    }
}
