using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetInventoriesByUserId
{
    public record GetInventoriesByUserIdQuery(string UserId) : IRequest<IEnumerable<InventoryDto>>;    
}
