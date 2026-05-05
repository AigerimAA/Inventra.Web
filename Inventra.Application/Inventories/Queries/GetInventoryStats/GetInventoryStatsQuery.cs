using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Inventories.Queries.GetInventoryStats
{
    public record GetInventoryStatsQuery(int InventoryId) : IRequest<InventoryStatsDto>;
}
