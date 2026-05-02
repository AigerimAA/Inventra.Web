using AutoMapper;
using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomIdGenerator _customIdGenerator;

        public CreateItemCommandHandler(IItemRepository itemRepository, 
                IUnitOfWork unitOfWork, IMapper mapper, ICustomIdGenerator customIdGenerator)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customIdGenerator = customIdGenerator;
        }

        public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var customId = await _customIdGenerator.GenerateAsync(request.InventoryId, cancellationToken);

            var item = new Item
            {
                InventoryId = request.InventoryId,
                CreatedById = request.CreatedById,
                CustomId = customId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

                CustomString1Value = request.CustomString1Value,
                CustomString2Value = request.CustomString2Value,
                CustomString3Value = request.CustomString3Value,

                CustomInt1Value = request.CustomInt1Value,
                CustomInt2Value = request.CustomInt2Value,
                CustomInt3Value = request.CustomInt3Value,

                CustomText1Value = request.CustomText1Value,
                CustomText2Value = request.CustomText2Value,
                CustomText3Value = request.CustomText3Value,

                CustomBool1Value = request.CustomBool1Value,
                CustomBool2Value = request.CustomBool2Value,
                CustomBool3Value = request.CustomBool3Value,

                CustomLink1Value = request.CustomLink1Value,
                CustomLink2Value = request.CustomLink2Value,
                CustomLink3Value = request.CustomLink3Value
            };

            await _itemRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ItemDto>(item);
        }
    }
}
