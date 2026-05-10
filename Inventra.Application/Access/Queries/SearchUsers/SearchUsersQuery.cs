using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Access.Queries.SearchUsers
{
    public record SearchUsersQuery(string Query) : IRequest<IEnumerable<AccessUserDto>>;
}

