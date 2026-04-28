using AutoMapper;
using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;

namespace Inventra.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return item is null ? null : _mapper.Map<ItemDto>(item);
        }

        public async Task<IEnumerable<ItemDto>> GetByInventoryIdAsync(int inventoryId)
        {
            var items = await _itemRepository.GetByInventoryIdAsync(inventoryId);
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }
        public async Task<ItemDto> CreateAsync(ItemDto dto, string createdById)
        {
            var item = _mapper.Map<Item>(dto);
            item.CreatedById = createdById;
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;
            await _itemRepository.AddAsync(item);
            return _mapper.Map<ItemDto>(item);
        }
        public async Task UpdateAsync(ItemDto dto)
        {
            var item = _mapper.Map<Item>(dto);
            item.UpdatedAt = DateTime.UtcNow;
            await _itemRepository.UpdateAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }
    }
}
