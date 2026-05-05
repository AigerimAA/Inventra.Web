namespace Inventra.Application.DTOs
{
    public class CustomIdElementDto
    {
        public int Id { get; set; }
        public string ElementType { get; set; } = string.Empty;
        public string? FormatString { get; set; }
        public string? FixedValue { get; set; }
        public int SortOrder { get; set; }
    }
}
