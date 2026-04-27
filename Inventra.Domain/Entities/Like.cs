using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Like
    {
        public int ItemId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Item Item { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
