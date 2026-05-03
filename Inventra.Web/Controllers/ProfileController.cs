using Inventra.Application.Inventories.Queries.GetAllInventories;
using Inventra.Application.Inventories.Queries.GetInventoriesByUserId;
using Inventra.Application.Inventories.Queries.GetInventoriesWithAccess;
using Inventra.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User)!;

            var ownedInventories = await _mediator.Send(new GetInventoriesByUserIdQuery(userId));
            var accessibleInventories = await _mediator.Send(new GetInventoriesWithAccessQuery(userId));

            ViewBag.OwnedInventories = ownedInventories;
            ViewBag.AccessibleInventories = accessibleInventories;

            return View();
        }
    }
}
