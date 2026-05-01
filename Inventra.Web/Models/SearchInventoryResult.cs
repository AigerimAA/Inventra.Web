namespace Inventra.Web.Models
{
    public class SearchInventoryResult
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? OwnerName { get; set; }
        public int ItemsCount { get; set; }
    }
}
