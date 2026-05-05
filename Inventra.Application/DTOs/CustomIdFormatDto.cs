namespace Inventra.Application.DTOs
{
    public class CustomIdFormatDto
    {
        public int InventoryId { get; set; }
        public List<CustomIdElementDto> Elements { get; set; } = new();
    }
}
