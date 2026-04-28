namespace Inventra.Application.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string CustomId { get; set; } = string.Empty;
        public int InventoryId { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public string? CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[] Version { get; set; } = [];

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

        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
    }
}
