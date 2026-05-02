using Inventra.Application.Items.Commands.CreateItem;
using Inventra.Application.Items.Commands.DeleteItem;
using Inventra.Application.Items.Queries.GetItemById;
using Inventra.Application.Inventories.Queries.GetInventoryById;
using Inventra.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public ItemController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task<IActionResult> Create(int inventoryId)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery(inventoryId));
            if (inventory == null)
                return NotFound();

            ViewBag.Inventory = inventory;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateItemCommand command)
        {
            if (!ModelState.IsValid)
            {
                var inventory = await _mediator.Send(new GetInventoryByIdQuery(command.InventoryId));
                ViewBag.Inventory = inventory;
                return View(command);
            }

            var userId = _userManager.GetUserId(User)!;
            var commandWithUser = new CreateItemCommand
            {
                InventoryId = command.InventoryId,
                CreatedById = userId,
                CustomString1Value = command.CustomString1Value,
                CustomString2Value = command.CustomString2Value,
                CustomString3Value = command.CustomString3Value,
                CustomInt1Value = command.CustomInt1Value,
                CustomInt2Value = command.CustomInt2Value,
                CustomInt3Value = command.CustomInt3Value,
                CustomText1Value = command.CustomText1Value,
                CustomText2Value = command.CustomText2Value,
                CustomText3Value = command.CustomText3Value,
                CustomBool1Value = command.CustomBool1Value,
                CustomBool2Value = command.CustomBool2Value,
                CustomBool3Value = command.CustomBool3Value,
                CustomLink1Value = command.CustomLink1Value,
                CustomLink2Value = command.CustomLink2Value,
                CustomLink3Value = command.CustomLink3Value
            };

            await _mediator.Send(commandWithUser);
            return RedirectToAction("Details", "Inventory",
                new {id = command.InventoryId});
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int inventoryId)
        {
            await _mediator.Send(new DeleteItemCommand(id));
            return RedirectToAction("Details", "Inventory", new {id = inventoryId});
        }
    }
}
