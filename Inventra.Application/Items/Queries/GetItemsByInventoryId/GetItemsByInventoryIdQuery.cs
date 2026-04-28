using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Items.Queries.GetItemsByInventoryId
{
    public record GetItemsByInventoryIdQuery(int InventoryId) : IRequest<IEnumerable<ItemDto>>;
}
