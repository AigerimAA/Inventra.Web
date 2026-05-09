namespace Inventra.Web.Models
{
    public class SaveImageUrlRequest
    {
        public int InventoryId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
