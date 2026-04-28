using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<IEnumerable<ApplicationUser>> SearchByNameOrEmailAsync(string query)
        {
            return await _context.Users
                .Where(u => u.UserName!.Contains(query) || u.Email!.Contains(query))
                .Take(10)
                .ToListAsync();
        }
        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
        }
    }
}
