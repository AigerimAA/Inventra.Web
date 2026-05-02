using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikeController(ILikeRepository likeRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _likeRepository = likeRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int itemId, int inventoryId)
        {
            var userId = _userManager.GetUserId(User)!;

            var existing = await _likeRepository.GetByUserAndItemAsync(itemId, userId);

            if (existing != null)
                await _likeRepository.RemoveAsync(existing);
            else
                await _likeRepository.AddAsync(new Like { ItemId = itemId, UserId = userId });

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Details", "Inventory", new {id = inventoryId});
        }
    }
}
