using Microsoft.AspNetCore.Identity;

namespace Inventra.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "light";
        public bool IsBlocked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Inventory> OwnedInventories { get; set; } = new List<Inventory>();
        public ICollection<InventoryAccess> InventoryAccesses { get; set; } = new List<InventoryAccess>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
