using System.Runtime.CompilerServices;
using AutoMapper;
using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;

namespace Inventra.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<InventoryDto?> GetByIdAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            return inventory is null ? null : _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InventoryDto>>(inventories);
        }
        public async Task<IEnumerable<InventoryDto>> GetByOwnerIdAsync(string ownerId)
        {
            var inventories = await _inventoryRepository.GetByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<InventoryDto>>(inventories);
        }
        public async Task<InventoryDto> CreateAsync(InventoryDto dto, string ownerId)
        {
            var inventory = _mapper.Map<Inventory>(dto);
            inventory.OwnerId = ownerId;
            inventory.CreatedAt = DateTime.UtcNow;
            inventory.UpdatedAt = DateTime.UtcNow;
            await _inventoryRepository.AddAsync(inventory);
            return _mapper.Map<InventoryDto>(inventory);
        }
        public async Task UpdateAsync(InventoryDto dto)
        {
            var inventory = _mapper.Map<Inventory>(dto);
            inventory.UpdatedAt = DateTime.UtcNow;
            await _inventoryRepository.UpdateAsync(inventory);
        }
        public async Task DeleteAsync(int id)
        {
            await _inventoryRepository.DeleteAsync(id);
        }
    }
}
