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

        public UpdateItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
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

            try
            {
                await _itemRepository.UpdateAsync(item);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception)
            {
                throw new ConcurrencyException();
            }
        }
    }
}
