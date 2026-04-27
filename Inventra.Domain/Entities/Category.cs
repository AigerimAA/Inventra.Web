using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
