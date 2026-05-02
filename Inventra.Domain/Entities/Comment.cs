namespace Inventra.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int InventoryId { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public Inventory Inventory { get; set; } = null!;
        public ApplicationUser Author { get; set; } =null!;
    }
}
