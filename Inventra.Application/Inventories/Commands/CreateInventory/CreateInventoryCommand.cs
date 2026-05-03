using MediatR;
using Inventra.Application.DTOs;

namespace Inventra.Application.Inventories.Commands.CreateInventory
{
    public record class CreateInventoryCommand : IRequest<InventoryDto>
    {
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public bool IsPublic { get; init; }
        public int CategoryId { get; init; }
        public string OwnerId { get; init; } = string.Empty;
        public IList<string> Tags { get; init; } = new List<string>();
    }
}
