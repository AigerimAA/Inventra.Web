namespace Inventra.Web.Models
{
    public class AutoSaveInventoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublic { get; set; }
        public int CategoryId { get; set; }
        public string Version { get; set; } = string.Empty;
        public List<string>? Tags { get; set; }
        public string? CustomString1Name { get; set; }
        public bool CustomString1Shown { get; set; }
        public string? CustomString2Name { get; set; }
        public bool CustomString2Shown { get; set; }
        public string? CustomString3Name { get; set; }
        public bool CustomString3Shown { get; set; }
        public string? CustomInt1Name { get; set; }
        public bool CustomInt1Shown { get; set; }
        public string? CustomInt2Name { get; set; }
        public bool CustomInt2Shown { get; set; }
        public string? CustomInt3Name { get; set; }
        public bool CustomInt3Shown { get; set; }
        public string? CustomText1Name { get; set; }
        public bool CustomText1Shown { get; set; }
        public string? CustomText2Name { get; set; }
        public bool CustomText2Shown { get; set; }
        public string? CustomText3Name { get; set; }
        public bool CustomText3Shown { get; set; }
        public string? CustomBool1Name { get; set; }
        public bool CustomBool1Shown { get; set; }
        public string? CustomBool2Name { get; set; }
        public bool CustomBool2Shown { get; set; }
        public string? CustomBool3Name { get; set; }
        public bool CustomBool3Shown { get; set; }
        public string? CustomLink1Name { get; set; }
        public bool CustomLink1Shown { get; set; }
        public string? CustomLink2Name { get; set; }
        public bool CustomLink2Shown { get; set; }
        public string? CustomLink3Name { get; set; }
        public bool CustomLink3Shown { get; set; }
    }
}
