using Inventra.Application.Common.Exceptions;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IInventoryPermissionService _permissionService;

        public UpdateItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService, IInventoryPermissionService permissionService)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            if (!await _permissionService.CanWriteAsync(userId, _currentUserService.IsAdmin, request.InventoryId))
                throw new UnauthorizedAccessException("You do not have write access to this inventory");

            var item = await _itemRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Item), request.Id);

            await _itemRepository.SetOriginalVersionAsync(item, request.Version);

            item.UpdatedAt = DateTime.UtcNow;
            item.CustomString1Value = request.CustomString1Value;
            item.CustomString2Value = request.CustomString2Value;
            item.CustomString3Value = request.CustomString3Value;
            item.CustomInt1Value = request.CustomInt1Value;
            item.CustomInt2Value = request.CustomInt2Value;
            item.CustomInt3Value = request.CustomInt3Value;
            item.CustomText1Value = request.CustomText1Value;
            item.CustomText2Value = request.CustomText2Value;
            item.CustomText3Value = request.CustomText3Value;
            item.CustomBool1Value = request.CustomBool1Value;
            item.CustomBool2Value = request.CustomBool2Value;
            item.CustomBool3Value = request.CustomBool3Value;
            item.CustomLink1Value = request.CustomLink1Value;
            item.CustomLink2Value = request.CustomLink2Value;
            item.CustomLink3Value = request.CustomLink3Value;

            await _itemRepository.UpdateAsync(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
