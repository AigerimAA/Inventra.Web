using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetByPrefixAsync(string prefix, int maxResults = 10)
        {
            return await _context.Tags
                .Where(t => EF.Functions.Like(t.Name, prefix + "%"))
                .OrderBy(t => t.Name)
                .Take(maxResults)
                .ToListAsync();
        }
        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }
        public void RemoveInventoryTags(IList<InventoryTag> inventoryTags)
        {
            _context.InventoryTags.RemoveRange(inventoryTags);
        }
        public async Task<Tag> GetOrCreateAsync(string name)
        {
            var existing = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);

            if (existing != null) return existing;

            var tag = new Tag { Name = name };
            _context.Tags.Add(tag);

            try
            {
                await _context.SaveChangesAsync();
                return tag;
            }
            catch (DbUpdateException)
            {
                _context.Entry(tag).State = EntityState.Detached;

                return await _context.Tags.FirstAsync(t => t.Name == name);
            }
        }
        public async Task<IEnumerable<TagWithCount>> GetTagsWithCountAsync(int maxTags = 50, CancellationToken cancellationToken = default)
            => await _context.InventoryTags
                .AsNoTracking()
                .Include(it => it.Tag)
                .GroupBy(it => new { it.TagId, it.Tag.Name })
                .Select(g => new TagWithCount { Name = g.Key.Name, Count = g.Count() })
                .OrderByDescending(t => t.Count)
                .Take(maxTags)
                .ToListAsync(cancellationToken);
    }
}
