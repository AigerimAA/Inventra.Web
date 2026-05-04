using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Access.Queries
{
    public class GetInventoryUsersQueryHandler : IRequestHandler<GetInventoryUsersQuery, IEnumerable<AccessUserDto>>
    {
        private readonly IAccessRepository _accessRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInventoryPermissionService _permissionService;
        public GetInventoryUsersQueryHandler(IAccessRepository accessRepository, ICurrentUserService currentUserService, IInventoryPermissionService permissionService)
        { 
            _accessRepository = accessRepository; 
            _currentUserService = currentUserService; 
            _permissionService = permissionService; 
        }
        public async Task<IEnumerable<AccessUserDto>> Handle(GetInventoryUsersQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
            if (!await _permissionService.CanManageAsync(userId, _currentUserService.IsAdmin, request.InventoryId))
                throw new UnauthorizedAccessException();

            var accesses = await _accessRepository.GetUsersWithAccessAsync(request.InventoryId, cancellationToken);
            return accesses.Select(a => new AccessUserDto
            {
                Id = a.User.Id,
                UserName = a.User.UserName!,
                Email = a.User.Email!
            });
        }
    }
}
