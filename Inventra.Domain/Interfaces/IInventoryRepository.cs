using System;
using System.Collections.Generic;
using System.Text;
using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(int id);
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<IEnumerable<Inventory>> GetByOwnerIdAsync(string OwnerId);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(int id);
    }
}
