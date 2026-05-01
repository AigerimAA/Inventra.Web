namespace Inventra.Web.Models
{
    public class SearchResultViewModel
    {
        public string Query { get; set; } = string.Empty;
        public List<SearchInventoryResult> Inventories { get; set; } = new();
        public List<SearchItemResult> Items { get; set; } = new();
    }
}
