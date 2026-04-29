using Inventra.Application.Inventories.Commands.CreateInventory;
using Inventra.Application.Inventories.Commands.DeleteInventory;
using Inventra.Application.Inventories.Queries.GetAllInventories;
using Inventra.Application.Inventories.Queries.GetInventoryById;
using Inventra.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public InventoryController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _mediator.Send(new GetAllInventoriesQuery());
            return View(inventories);
        }
        public async Task<IActionResult> Details(int id)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery(id));
            if (inventory == null)
                return NotFound();
            return View(inventory);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInventoryCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var userId = _userManager.GetUserId(User)!;
            var commandWithOwner = new CreateInventoryCommand
            {
                Title = command.Title,
                Description = command.Description,
                ImageUrl = command.ImageUrl,
                IsPublic = command.IsPublic,
                CategoryId = command.CategoryId,
                OwnerId = userId,
                Tags = command.Tags
            };

            var result = await _mediator.Send(commandWithOwner);
            return RedirectToAction(nameof(Details), new { id = result.Id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery(id));
            if (inventory == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (inventory.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();

            return View(inventory);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteInventoryCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
