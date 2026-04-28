using Inventra.Application.DTOs;

namespace Inventra.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryDto?> GetByIdAsync(int id);
        Task<IEnumerable<InventoryDto>> GetAllAsync();
        Task<IEnumerable<InventoryDto>> GetByOwnerIdAsync(string ownerId);
        Task<InventoryDto> CreateAsync(InventoryDto dto, string ownerId);
        Task UpdateAsync(InventoryDto dto);
        Task DeleteAsync(int id);
    }
}
