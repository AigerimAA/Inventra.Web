namespace Inventra.Application.DTOs
{
    public class SearchItemDto
    {
        public int Id { get; init; }
        public string CustomId { get; init; } = string.Empty;
        public int InventoryId { get; init; }
        public string? InventoryTitle { get; init; }
        public string? CreatedByName { get; init; }
    }
}
