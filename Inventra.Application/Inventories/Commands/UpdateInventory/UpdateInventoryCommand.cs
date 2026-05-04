using MediatR;

namespace Inventra.Application.Inventories.Commands.UpdateInventory
{
    public class UpdateInventoryCommand : IRequest<byte[]>
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public bool IsPublic { get; init; }
        public int CategoryId { get; init; }
        public byte[] Version { get; init; } = [];
        public IList<string> Tags { get; init; } = new List<string>();

        public string? CustomString1Name { get; init; }
        public bool CustomString1Shown { get; init; }
        public string? CustomString2Name { get; init; }
        public bool CustomString2Shown { get; init; }
        public string? CustomString3Name { get; init; }
        public bool CustomString3Shown { get; init; }

        public string? CustomInt1Name { get; init; }
        public bool CustomInt1Shown { get; init; }
        public string? CustomInt2Name { get; init; }
        public bool CustomInt2Shown { get; init; }
        public string? CustomInt3Name { get; init; }
        public bool CustomInt3Shown { get; init; }

        public string? CustomText1Name { get; init; }
        public bool CustomText1Shown { get; init; }
        public string? CustomText2Name { get; init; }
        public bool CustomText2Shown { get; init; }
        public string? CustomText3Name { get; init; }
        public bool CustomText3Shown { get; init; }

        public string? CustomBool1Name { get; init; }
        public bool CustomBool1Shown { get; init; }
        public string? CustomBool2Name { get; init; }
        public bool CustomBool2Shown { get; init; }
        public string? CustomBool3Name { get; init; }
        public bool CustomBool3Shown { get; init; }

        public string? CustomLink1Name { get; init; }
        public bool CustomLink1Shown { get; init; }
        public string? CustomLink2Name { get; init; }
        public bool CustomLink2Shown { get; init; }
        public string? CustomLink3Name { get; init; }
        public bool CustomLink3Shown { get; init; }
    }
}
