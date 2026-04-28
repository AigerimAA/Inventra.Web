using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string CustomId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] Version { get; set; } = [];
        public int InventoryId { get; set; }
        public string CreatedById { get; set; } = string.Empty;

        public string? CustomString1Value { get; set; }
        public string? CustomString2Value { get; set; }
        public string? CustomString3Value { get; set; }

        public decimal? CustomInt1Value { get; set; }
        public decimal? CustomInt2Value { get; set; }
        public decimal? CustomInt3Value { get; set; }
        public string? CustomText1Value { get; set; }
        public string? CustomText2Value { get; set; }
        public string? CustomText3Value { get; set; }
        public bool? CustomBool1Value { get; set; }
        public bool? CustomBool2Value { get; set; }
        public bool? CustomBool3Value { get; set; }
        public string? CustomLink1Value { get; set; }
        public string? CustomLink2Value { get; set; }
        public string? CustomLink3Value { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
