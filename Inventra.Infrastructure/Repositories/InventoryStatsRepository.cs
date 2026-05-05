using Inventra.Domain.Interfaces;
using Inventra.Domain.ValueObjects;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class InventoryStatsRepository : IInventoryStatsRepository
    {
        private readonly AppDbContext _context;
        public InventoryStatsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<InventoryStats> GetStatsAsync(int inventoryId, CancellationToken cancellationToken = default)
        {
            var q = _context.Items.AsNoTracking().Where(i => i.InventoryId == inventoryId);
            var stats = new InventoryStats { TotalItems = await q.CountAsync(cancellationToken) };
            if (stats.TotalItems == 0) return stats;

            var int1Stats = await q
                .Where(i => i.CustomInt1Value.HasValue)
                .GroupBy(i => 1)
                .Select(g => new { Avg = g.Average(x => x.CustomInt1Value), Min = g.Min(x => x.CustomInt1Value), Max = g.Max(x => x.CustomInt1Value) })
                .FirstOrDefaultAsync(cancellationToken);
            if (int1Stats != null) { stats.Int1Avg = int1Stats.Avg.HasValue ? Math.Round(int1Stats.Avg.Value, 2) : null; stats.Int1Min = int1Stats.Min; stats.Int1Max = int1Stats.Max; }

            var int2Stats = await q
                .Where(i => i.CustomInt2Value.HasValue)
                .GroupBy(i => 1)
                .Select(g => new { Avg = g.Average(x => x.CustomInt2Value), Min = g.Min(x => x.CustomInt2Value), Max = g.Max(x => x.CustomInt2Value) })
                .FirstOrDefaultAsync(cancellationToken);
            if (int2Stats != null) { stats.Int2Avg = int2Stats.Avg.HasValue ? Math.Round(int2Stats.Avg.Value, 2) : null; stats.Int2Min = int2Stats.Min; stats.Int2Max = int2Stats.Max; }

            var int3Stats = await q
                .Where(i => i.CustomInt3Value.HasValue)
                .GroupBy(i => 1)
                .Select(g => new { Avg = g.Average(x => x.CustomInt3Value), Min = g.Min(x => x.CustomInt3Value), Max = g.Max(x => x.CustomInt3Value) })
                .FirstOrDefaultAsync(cancellationToken);
            if (int3Stats != null) { stats.Int3Avg = int3Stats.Avg.HasValue ? Math.Round(int3Stats.Avg.Value, 2) : null; stats.Int3Min = int3Stats.Min; stats.Int3Max = int3Stats.Max; }

            var str1Top = await q
                .Where(i => !string.IsNullOrWhiteSpace(i.CustomString1Value))
                .GroupBy(i => i.CustomString1Value)
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefaultAsync(cancellationToken);
            if (str1Top != null) { stats.String1TopValue = str1Top.Value; stats.String1TopCount = str1Top.Count; }

            var str2Top = await q
                .Where(i => !string.IsNullOrWhiteSpace(i.CustomString2Value))
                .GroupBy(i => i.CustomString2Value)
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefaultAsync(cancellationToken);
            if (str2Top != null) { stats.String2TopValue = str2Top.Value; stats.String2TopCount = str2Top.Count; }

            var str3Top = await q
                .Where(i => !string.IsNullOrWhiteSpace(i.CustomString3Value))
                .GroupBy(i => i.CustomString3Value)
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefaultAsync(cancellationToken);
            if (str3Top != null) { stats.String3TopValue = str3Top.Value; stats.String3TopCount = str3Top.Count; }

            return stats;
        }
    }
}
