using Inventra.Application.Common.Exceptions;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Inventories.Commands.DeleteInventory
{
    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInventoryCommandHandler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Inventory), request.Id);

            await _inventoryRepository.DeleteAsync(inventory.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
