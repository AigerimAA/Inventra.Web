namespace Inventra.Application.DTOs
{
    public class SearchResultDto
    {
        public IEnumerable<InventoryDto> Inventories { get; init; } = [];
        public IEnumerable<SearchItemDto> Items { get; init; } = [];
    }
}
