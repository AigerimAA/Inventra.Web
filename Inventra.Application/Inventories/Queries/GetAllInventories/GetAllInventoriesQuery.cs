using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetAllInventories
{
    public record GetAllInventoriesQuery : IRequest<IEnumerable<InventoryDto>>;    
}
