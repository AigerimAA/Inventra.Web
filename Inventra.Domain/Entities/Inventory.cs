using System.ComponentModel.DataAnnotations;

namespace Inventra.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(2000)]
        public string? ImageUrl { get; set; }

        public bool IsPublic { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] Version { get; set; } = null!;

        public string OwnerId { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        [MaxLength(200)] public string? CustomString1Name { get; set; }
        public bool CustomString1Shown { get; set; }
        [MaxLength(200)] public string? CustomString2Name { get; set; }
        public bool CustomString2Shown { get; set; }
        [MaxLength(200)] public string? CustomString3Name { get; set; }
        public bool CustomString3Shown { get; set; }

        [MaxLength(200)] public string? CustomInt1Name { get; set; }
        public bool CustomInt1Shown { get; set; }
        [MaxLength(200)] public string? CustomInt2Name { get; set; }
        public bool CustomInt2Shown { get; set; }
        [MaxLength(200)] public string? CustomInt3Name { get; set; }
        public bool CustomInt3Shown { get; set; }

        [MaxLength(200)] public string? CustomText1Name { get; set; }
        public bool CustomText1Shown { get; set; }
        [MaxLength(200)] public string? CustomText2Name { get; set; }
        public bool CustomText2Shown { get; set; }
        [MaxLength(200)] public string? CustomText3Name { get; set; }
        public bool CustomText3Shown { get; set; }

        [MaxLength(200)] public string? CustomBool1Name { get; set; }
        public bool CustomBool1Shown { get; set; }
        [MaxLength(200)] public string? CustomBool2Name { get; set; }
        public bool CustomBool2Shown { get; set; }
        [MaxLength(200)] public string? CustomBool3Name { get; set; }
        public bool CustomBool3Shown { get; set; }

        [MaxLength(200)] public string? CustomLink1Name { get; set; }
        public bool CustomLink1Shown { get; set; }
        [MaxLength(200)] public string? CustomLink2Name { get; set; }
        public bool CustomLink2Shown { get; set; }
        [MaxLength(200)] public string? CustomLink3Name { get; set; }
        public bool CustomLink3Shown { get; set; }

        public ApplicationUser Owner { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<InventoryTag> InventoryTags { get; set; } = new List<InventoryTag>();
        public ICollection<InventoryAccess> AccessList { get; set; } = new List<InventoryAccess>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public CustomIdFormat? CustomIdFormat { get; set; }
        public InventorySequence? Sequence { get; set; }
    }
}