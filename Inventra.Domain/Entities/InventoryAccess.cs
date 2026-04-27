using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class InventoryAccess
    {
        public int InventoryId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Inventory Inventory { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
