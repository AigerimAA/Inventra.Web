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

        public async Task<IEnumerable<InventoryAccess>> GetUsersWithAccessAsync(int inventoryId, CancellationToken cancellationToken = default)
            => await _context.InventoryAccesses
                .Where(a => a.InventoryId == inventoryId)
                .Include(a => a.User)
                .ToListAsync(cancellationToken);

        public async Task<InventoryAccess> AddAccessAsync(int inventoryId, string userId, CancellationToken cancellationToken = default)
        {
            var exists = await _context.InventoryAccesses.AnyAsync(a => a.InventoryId == inventoryId && a.UserId == userId, cancellationToken);
            if (exists) throw new InvalidOperationException("User already has access");
            var access = new InventoryAccess { InventoryId = inventoryId, UserId = userId };
            
            _context.InventoryAccesses.Add(access);
            await _context.SaveChangesAsync(cancellationToken);
            return await _context.InventoryAccesses
                .Include(a => a.User)
                .FirstAsync(a => a.InventoryId == inventoryId && a.UserId == userId, cancellationToken);
        }

        public async Task RemoveAccessAsync(int inventoryId, string userId, CancellationToken cancellationToken = default)
        {
            var access = await _context.InventoryAccesses
                .FirstOrDefaultAsync(a => a.InventoryId == inventoryId && a.UserId == userId, cancellationToken);
            if (access != null) { _context.InventoryAccesses.Remove(access); await _context.SaveChangesAsync(cancellationToken); }
        }

        public async Task<IEnumerable<(string Id, string UserName, string Email)>> SearchUsersAsync(string query, CancellationToken cancellationToken = default)
            => await _context.Users
        .Where(u => EF.Functions.Like(u.UserName!, $"%{query}%") || EF.Functions.Like(u.Email!, $"%{query}%"))
        .Take(10)
        .Select(u => ValueTuple.Create(u.Id, u.UserName!, u.Email!))
        .ToListAsync(cancellationToken);
    }
}
