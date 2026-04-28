using AutoMapper;
using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Inventories.Commands.CreateInventory
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, InventoryDto>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InventoryDto> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = new Inventory
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsPublic = request.IsPublic,
                CategoryId = request.CategoryId,
                OwnerId = request.OwnerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<InventoryDto>(inventory);
        }
    }
}
