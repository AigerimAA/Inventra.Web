using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetInventoryById
{
    public record GetInventoryByIdQuery(int Id) : IRequest<InventoryDto>;
    
}
