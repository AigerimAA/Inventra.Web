using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Access.Queries
{
    public record GetInventoryUsersQuery(int InventoryId) : IRequest<IEnumerable<AccessUserDto>>;
}
