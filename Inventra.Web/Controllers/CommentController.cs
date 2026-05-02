using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ICommentRepository commentRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
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

            await _commentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Details", "Inventory", new {id = inventoryId});
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int inventoryId)
        {
            var comments = await _commentRepository.GetByInventoryIdAsync(inventoryId);

            var result = comments.Select(c => new
            {
                authorName = c.Author?.UserName,
                content = c.Content,
                createdAt = c.CreatedAt.ToString("dd.MM.yyyy HH:mm")
            });

            return Json(result);
        }
    }
}
