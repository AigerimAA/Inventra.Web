using Inventra.Application.Common.Exceptions;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Items.Commands.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInventoryPermissionService _permissionService;

        public DeleteItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService, IInventoryPermissionService permissionService)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var item = await _itemRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Item), request.Id);

            var userId = _currentUserService.UserId!;
            if (!await _permissionService.CanWriteAsync(userId, item.InventoryId))
                throw new UnauthorizedAccessException("You do not have write access to this inventory");

            await _itemRepository.DeleteAsync(item.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
