using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IIdentityService _identityService;

        public AdminController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _identityService.GetAllUsersWithRolesAsync();
            var userViewModels = users.Select(u => new AdminUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsBlocked = u.IsBlocked,
                IsAdmin = u.IsAdmin,
                CreatedAt = u.CreatedAt
            });
            return View(userViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleBlock(string userId)
        {
            var user = await _identityService.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.IsBlocked = !user.IsBlocked;
            await _identityService.UpdateUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string userId)
        {
            var user = await _identityService.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var isAdmin = await _identityService.IsInRoleAsync(user, "Admin");
            if (isAdmin)
                await _identityService.RemoveFromRoleAsync(user, "Admin");
            else
                await _identityService.AddToRoleAsync(user, "Admin");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _identityService.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _identityService.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
