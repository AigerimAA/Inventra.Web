namespace Inventra.Domain.ValueObjects
{
    public class InventoryStats
    {
        public int TotalItems { get; set; }
        public decimal? Int1Avg { get; set; }
        public decimal? Int1Min { get; set; }
        public decimal? Int1Max { get; set; }
        public decimal? Int2Avg { get; set; }
        public decimal? Int2Min { get; set; }
        public decimal? Int2Max { get; set; }
        public decimal? Int3Avg { get; set; }
        public decimal? Int3Min { get; set; }
        public decimal? Int3Max { get; set; }
        public string? String1TopValue { get; set; }
        public int String1TopCount { get; set; }
        public string? String2TopValue { get; set; }
        public int String2TopCount { get; set; }
        public string? String3TopValue { get; set; }
        public int String3TopCount { get; set; }
    }
}
