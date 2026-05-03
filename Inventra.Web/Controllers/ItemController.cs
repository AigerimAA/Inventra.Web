using Inventra.Application.Items.Commands.CreateItem;
using Inventra.Application.Items.Commands.DeleteItem;
using Inventra.Application.Items.Queries.GetItemById;
using Inventra.Application.Inventories.Queries.GetInventoryById;
using Inventra.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Inventra.Application.Interfaces;

namespace Inventra.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInventoryPermissionService _permissionService;
        private readonly ICurrentUserService _currentUserService;

        public ItemController(IMediator mediator, UserManager<ApplicationUser> userManager,
            IInventoryPermissionService permissionService, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _permissionService = permissionService;
            _currentUserService = currentUserService;
        }
        public async Task<IActionResult> Create(int inventoryId)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return Forbid();

            if (!await _permissionService.CanWriteAsync(
                    userId, _currentUserService.IsAdmin, inventoryId))
                return Forbid();

            var inventory = await _mediator.Send(new GetInventoryByIdQuery(inventoryId));
            if (inventory == null) return NotFound();

            ViewBag.Inventory = inventory;
            return View(new CreateItemCommand { InventoryId = inventoryId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateItemCommand command)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return Forbid();

            if (!await _permissionService.CanWriteAsync(
                    userId, _currentUserService.IsAdmin, command.InventoryId))
                return Forbid();

            if (!ModelState.IsValid)
            {
                var inv = await _mediator.Send(
                    new GetInventoryByIdQuery(command.InventoryId));
                ViewBag.Inventory = inv;
                return View(command);
            }

            var commandWithUser = command with { CreatedById = userId };

            await _mediator.Send(commandWithUser);
            return RedirectToAction("Details", "Inventory",
                new { id = command.InventoryId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int inventoryId)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return Forbid();

            if (!await _permissionService.CanWriteAsync(
                    userId, _currentUserService.IsAdmin, inventoryId))
                return Forbid();

            await _mediator.Send(new DeleteItemCommand(id));
            return RedirectToAction("Details", "Inventory", new { id = inventoryId });
        }
    }
}
