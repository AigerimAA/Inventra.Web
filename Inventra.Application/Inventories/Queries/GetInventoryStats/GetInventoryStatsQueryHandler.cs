using Inventra.Application.DTOs;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetInventoryStats
{
    public class GetInventoryStatsQueryHandler : IRequestHandler<GetInventoryStatsQuery, InventoryStatsDto>
    {
        private readonly IItemRepository _itemRepository;
        public GetInventoryStatsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<InventoryStatsDto> Handle(GetInventoryStatsQuery request, CancellationToken cancellationToken)
        {
            var items = (await _itemRepository.GetByInventoryIdAsync(request.InventoryId)).ToList();

            var stats = new InventoryStatsDto { TotalItems = items.Count };

            if (items.Count == 0) return stats;

            var int1Values = items.Where(i => i.CustomInt1Value.HasValue).Select(i => i.CustomInt1Value!.Value).ToList();
            if (int1Values.Any()) { stats.Int1Avg = Math.Round(int1Values.Average(), 2); stats.Int1Min = int1Values.Min(); stats.Int1Max = int1Values.Max(); }

            var int2Values = items.Where(i => i.CustomInt2Value.HasValue).Select(i => i.CustomInt2Value!.Value).ToList();
            if (int2Values.Any()) { stats.Int2Avg = Math.Round(int2Values.Average(), 2); stats.Int2Min = int2Values.Min(); stats.Int2Max = int2Values.Max(); }

            var int3Values = items.Where(i => i.CustomInt3Value.HasValue).Select(i => i.CustomInt3Value!.Value).ToList();
            if (int3Values.Any()) { stats.Int3Avg = Math.Round(int3Values.Average(), 2); stats.Int3Min = int3Values.Min(); stats.Int3Max = int3Values.Max(); }

            var str1Top = items.Where(i => !string.IsNullOrEmpty(i.CustomString1Value)).GroupBy(i => i.CustomString1Value).OrderByDescending(g => g.Count()).FirstOrDefault();
            if (str1Top != null) { stats.String1TopValue = str1Top.Key; stats.String1TopCount = str1Top.Count(); }

            var str2Top = items.Where(i => !string.IsNullOrEmpty(i.CustomString2Value)).GroupBy(i => i.CustomString2Value).OrderByDescending(g => g.Count()).FirstOrDefault();
            if (str2Top != null) { stats.String2TopValue = str2Top.Key; stats.String2TopCount = str2Top.Count(); }

            var str3Top = items.Where(i => !string.IsNullOrEmpty(i.CustomString3Value)).GroupBy(i => i.CustomString3Value).OrderByDescending(g => g.Count()).FirstOrDefault();
            if (str3Top != null) { stats.String3TopValue = str3Top.Key; stats.String3TopCount = str3Top.Count(); }

            return stats;
        }
    }
}
