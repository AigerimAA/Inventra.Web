using MediatR;

namespace Inventra.Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public int Id { get; init; }
        public byte[] Version { get; init; } = [];

        public string? CustomString1Value { get; init; }
        public string? CustomString2Value { get; init; }
        public string? CustomString3Value { get; init; }

        public decimal? CustomInt1Value { get; init; }
        public decimal? CustomInt2Value { get; init; }
        public decimal? CustomInt3Value { get; init; }

        public string? CustomText1Value { get; init; }
        public string? CustomText2Value { get; init; }
        public string? CustomText3Value { get; init; }

        public bool? CustomBool1Value { get; init; }
        public bool? CustomBool2Value { get; init; }
        public bool? CustomBool3Value { get; init; }

        public string? CustomLink1Value { get; init; }
        public string? CustomLink2Value { get; init; }
        public string? CustomLink3Value { get; init; }
    }
}
