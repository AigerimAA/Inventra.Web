using Inventra.Infrastructure.Persistence;
using Inventra.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int inventoryId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return RedirectToAction("Details", "Inventory", new {id = inventoryId});

            var userId = _userManager.GetUserId(User)!;

            var comment = new Comment
            {
                InventoryId = inventoryId,
                AuthorId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Inventory", new {id = inventoryId});
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int inventoryId)
        {
            var comments = await _context.Comments
                .Include(c => c.Author)
                .Where(c => c.InventoryId == inventoryId)
                .OrderBy(c => c.CreatedAt)
                .Select(c => new
                {
                    authorName = c.Author.UserName,
                    content = c.Content,
                    createdAt = c.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                }).ToListAsync();

            return Json(comments);
        }
    }
}
