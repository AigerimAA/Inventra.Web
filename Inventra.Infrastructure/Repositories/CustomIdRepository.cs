using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class CustomIdRepository : ICustomIdRepository
    {
        private readonly AppDbContext _context;
        public CustomIdRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomIdFormat?> GetByInventoryIdAsync(int inventoryId, CancellationToken cancellationToken = default)
            => await _context.CustomIdFormats
                .AsNoTracking()
                .Include(f => f.Elements)
                .FirstOrDefaultAsync(f => f.InventoryId == inventoryId, cancellationToken);

        public async Task SaveAsync(CustomIdFormat format, CancellationToken cancellationToken = default)
        {
            var existing = await _context.CustomIdFormats
                .Include(f => f.Elements)
                .FirstOrDefaultAsync(f => f.InventoryId == format.InventoryId, cancellationToken);

            if (existing == null)
            {
                await _context.CustomIdFormats.AddAsync(format, cancellationToken);
            }
            else
            {
                _context.CustomIdElements.RemoveRange(existing.Elements);
                existing.Elements.Clear();
                existing.UpdatedAt = DateTime.UtcNow;
                foreach (var element in format.Elements)
                {
                    element.FormatId = existing.Id; 
                    existing.Elements.Add(element);
                }
            }
        }
    }
}
