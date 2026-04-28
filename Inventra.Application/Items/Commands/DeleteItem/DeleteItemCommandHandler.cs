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

        public DeleteItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Item), request.Id);

            await _itemRepository.DeleteAsync(item.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
