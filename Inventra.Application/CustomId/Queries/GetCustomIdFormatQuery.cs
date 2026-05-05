using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.CustomId.Queries
{
    public record GetCustomIdFormatQuery(int InventoryId) : IRequest<CustomIdFormatDto?>;
}
