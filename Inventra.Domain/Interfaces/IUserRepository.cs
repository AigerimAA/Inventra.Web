using Inventra.Domain.Entities;

namespace Inventra.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> SearchByNameOrEmailAsync(string query, CancellationToken cancellationToken = default);
        Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    }
}
