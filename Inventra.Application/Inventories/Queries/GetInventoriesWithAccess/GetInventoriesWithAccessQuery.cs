using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetInventoriesWithAccess
{
    public record GetInventoriesWithAccessQuery(string UserId) : IRequest<IEnumerable<InventoryDto>>;    
}
