using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> SearchByNameOrEmailAsync(string query);
        Task UpdateAsync(ApplicationUser user);
    }
}
