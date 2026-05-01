using Inventra.Infrastructure.Persistence;
using Inventra.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Web.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikeController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int itemId, int inventoryId)
        {
            var userId = _userManager.GetUserId(User)!;

            var existing = await _context.Likes
                .FirstOrDefaultAsync(l => l.ItemId == itemId && l.UserId == userId);

            if (existing != null)
                _context.Likes.Remove(existing);
            else
                _context.Likes.Add(new Like { ItemId = itemId, UserId = userId });

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Inventory", new {id = inventoryId});

        }
    }
}
