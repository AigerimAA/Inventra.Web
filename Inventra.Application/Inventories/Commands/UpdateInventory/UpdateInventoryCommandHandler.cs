using AutoMapper;
using Inventra.Application.Common.Exceptions;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Inventories.Commands.UpdateInventory
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInventoryCommandHandler(
            IInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Inventory), request.Id);

            inventory.Title = request.Title;
            inventory.Description = request.Description;
            inventory.ImageUrl = request.ImageUrl;
            inventory.IsPublic = request.IsPublic;
            inventory.CategoryId = request.CategoryId;
            inventory.UpdatedAt = DateTime.UtcNow;
            inventory.Version = request.Version;

            inventory.CustomString1Name = request.CustomString1Name;
            inventory.CustomString1Shown = request.CustomString1Shown;
            inventory.CustomString2Name = request.CustomString2Name;
            inventory.CustomString2Shown = request.CustomString2Shown;
            inventory.CustomString3Name = request.CustomString3Name;
            inventory.CustomString3Shown = request.CustomString3Shown;

            inventory.CustomInt1Name = request.CustomInt1Name;
            inventory.CustomInt1Shown = request.CustomInt1Shown;
            inventory.CustomInt2Name = request.CustomInt2Name;
            inventory.CustomInt2Shown = request.CustomInt2Shown;
            inventory.CustomInt3Name = request.CustomInt3Name;
            inventory.CustomInt3Shown = request.CustomInt3Shown;

            inventory.CustomText1Name = request.CustomText1Name;
            inventory.CustomText1Shown = request.CustomText1Shown;
            inventory.CustomText2Name = request.CustomText2Name;
            inventory.CustomText2Shown = request.CustomText2Shown;
            inventory.CustomText3Name = request.CustomText3Name;
            inventory.CustomText3Shown = request.CustomText3Shown;

            inventory.CustomBool1Name = request.CustomBool1Name;
            inventory.CustomBool1Shown = request.CustomBool1Shown;
            inventory.CustomBool2Name = request.CustomBool2Name;
            inventory.CustomBool2Shown = request.CustomBool2Shown;
            inventory.CustomBool3Name = request.CustomBool3Name;
            inventory.CustomBool3Shown = request.CustomBool3Shown;

            inventory.CustomLink1Name = request.CustomLink1Name;
            inventory.CustomLink1Shown = request.CustomLink1Shown;
            inventory.CustomLink2Name = request.CustomLink2Name;
            inventory.CustomLink2Shown = request.CustomLink2Shown;
            inventory.CustomLink3Name = request.CustomLink3Name;
            inventory.CustomLink3Shown = request.CustomLink3Shown;

            try
            {
                await _inventoryRepository.UpdateAsync(inventory);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception)
            {
                throw new ConcurrencyException();
            }
        }
    }
}
