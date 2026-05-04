using Inventra.Application.Interfaces;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Access.Commands.RemoveAccess
{
    public class RemoveAccessCommandHandler : IRequestHandler<RemoveAccessCommand>
    {
        private readonly IAccessRepository _accessRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInventoryPermissionService _permissionService;
        public RemoveAccessCommandHandler(IAccessRepository accessRepository, ICurrentUserService currentUserService, IInventoryPermissionService permissionService)
        { 
            _accessRepository = accessRepository; 
            _currentUserService = currentUserService; 
            _permissionService = permissionService; 
        }

        public async Task Handle(RemoveAccessCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
            if (!await _permissionService.CanManageAsync(userId, _currentUserService.IsAdmin, request.InventoryId))
                throw new UnauthorizedAccessException();
            await _accessRepository.RemoveAccessAsync(request.InventoryId, request.TargetUserId, cancellationToken);
        }
    }
}
