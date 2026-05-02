using MediatR;
using Inventra.Application.DTOs;

namespace Inventra.Application.Inventories.Queries.GetPopularInventories
{
    public record GetPopularInventoriesQuery(int Count = 5) : IRequest<IEnumerable<InventoryDto>>;
}
