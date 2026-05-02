namespace Inventra.Domain.Entities
{
    public class InventorySequence
    {
        public int InventoryId { get; set; }
        public int CurrentValue { get; set; }
        public Inventory Inventory { get; set; } = null!;
    }
}
