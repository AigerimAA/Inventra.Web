using Inventra.Application.DTOs;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Access.Queries
{
    public class GetInventoryUsersQueryHandler : IRequestHandler<GetInventoryUsersQuery, IEnumerable<AccessUserDto>>
    {
        private readonly IAccessRepository _accessRepository;
        public GetInventoryUsersQueryHandler(IAccessRepository accessRepository) => _accessRepository = accessRepository;
        public async Task<IEnumerable<AccessUserDto>> Handle(GetInventoryUsersQuery request, CancellationToken cancellationToken)
        {
            var accesses = await _accessRepository.GetUsersWithAccessAsync(request.InventoryId);
            return accesses.Select(a => new AccessUserDto
            {
                Id = a.User.Id,
                UserName = a.User.UserName!,
                Email = a.User.Email!
            });
        }
    }
}
