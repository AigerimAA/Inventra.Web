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
                .Where(t => t.Name.StartsWith(prefix))
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
    }
}
