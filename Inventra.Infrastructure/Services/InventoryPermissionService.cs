using Inventra.Application.Interfaces;
using Inventra.Domain.Constants;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Services
{
    public class InventoryPermissionService : IInventoryPermissionService
    {
        private readonly AppDbContext _context;

        public InventoryPermissionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CanWriteAsync(string userId, bool isAdmin, int inventoryId)
        {
            if (isAdmin) return true;

            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            if (inventory == null) return false;
            if (inventory.OwnerId == userId) return true;
            if (inventory.IsPublic) return true;

            return await _context.InventoryAccesses
                .AnyAsync(a => a.InventoryId == inventoryId && a.UserId == userId);
        }

        public async Task<bool> CanManageAsync(string userId, bool isAdmin, int inventoryId)
        {
            if (isAdmin) return true;

            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            return inventory?.OwnerId == userId;
        }
    }
}
