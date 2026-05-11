using Inventra.Application.DTOs;
using Inventra.Domain.Entities;

namespace Inventra.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync();
        Task<ApplicationUser?> FindByIdAsync(string userId);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<AuthResult> CreateUserAsync(ApplicationUser user, string password);
        Task<AuthResult> UpdateUserAsync(ApplicationUser user);
        Task<AuthResult> DeleteUserAsync(ApplicationUser user);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<AuthResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<AuthResult> RemoveFromRoleAsync(ApplicationUser user, string role);
        Task SignInAsync(ApplicationUser user, bool isPersistent);
        Task<AuthResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure);
        Task SignOutAsync();
        Task<string> GetExternalLoginRedirectUrlAsync(string provider, string redirectUrl);
        Task<(AuthResult Result, ApplicationUser? User)> ExternalLoginAsync();
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}