using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class CustomIdFormat
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public ICollection<CustomIdElement> Elements { get; set; } = new List<CustomIdElement>();

    }
}
