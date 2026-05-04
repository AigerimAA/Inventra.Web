using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class AccessRepository : IAccessRepository
    {
        private readonly AppDbContext _context;
        public AccessRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<InventoryAccess>> GetUsersWithAccessAsync(int inventoryId)
            => await _context.InventoryAccesses
                .Where(a => a.InventoryId == inventoryId)
                .Include(a => a.User)
                .ToListAsync();

        public async Task<InventoryAccess> AddAccessAsync(int inventoryId, string userId)
        {
            var exists = await _context.InventoryAccesses.AnyAsync(a => a.InventoryId == inventoryId && a.UserId == userId);
            if (exists) throw new InvalidOperationException("User already has access");
            var access = new InventoryAccess { InventoryId = inventoryId, UserId = userId };
            _context.InventoryAccesses.Add(access);
            await _context.SaveChangesAsync();
            return await _context.InventoryAccesses
                .Include(a => a.User)
                .FirstAsync(a => a.InventoryId == inventoryId && a.UserId == userId);
        }

        public async Task RemoveAccessAsync(int inventoryId, string userId)
        {
            var access = await _context.InventoryAccesses
                .FirstOrDefaultAsync(a => a.InventoryId == inventoryId && a.UserId == userId);
            if (access != null) { _context.InventoryAccesses.Remove(access); await _context.SaveChangesAsync(); }
        }

        public async Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string query)
            => await _context.Users
                .Where(u => EF.Functions.Like(u.UserName!, $"%{query}%") || EF.Functions.Like(u.Email!, $"%{query}%"))
                .Take(10)
                .ToListAsync();
    }
}
