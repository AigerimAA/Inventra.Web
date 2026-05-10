using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Access.Queries.GetInventoryUsers
{
    public record GetInventoryUsersQuery(int InventoryId) : IRequest<IEnumerable<AccessUserDto>>;
}
