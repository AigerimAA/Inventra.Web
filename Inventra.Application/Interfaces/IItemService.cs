using Inventra.Application.DTOs;

namespace Inventra.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto?> GetByIdAsync(int id);
        Task<IEnumerable<ItemDto>> GetByInventoryIdAsync(int inventoryId);
        Task<ItemDto> CreateAsync(ItemDto dto, string createdById);
        Task UpdateAsync(ItemDto dto);
        Task DeleteAsync(int id);
    }
}
