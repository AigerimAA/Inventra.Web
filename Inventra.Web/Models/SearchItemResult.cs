namespace Inventra.Web.Models
{
    public class SearchItemResult
    {
        public int Id { get; set; }
        public string CustomId { get; set; } = string.Empty;
        public int InventoryId { get; set; }
        public string? InventoryTitle { get; set; }
        public string? CreatedByName { get; set; }
    }
}
