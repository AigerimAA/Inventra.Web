using Inventra.Application.Categories.Queries.GetAllCategories;
using Inventra.Application.DTOs;
using Inventra.Application.Inventories.Commands.CreateInventory;
using Inventra.Application.Inventories.Commands.DeleteInventory;
using Inventra.Application.Inventories.Commands.UpdateInventory;
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
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");
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

            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Name");

            return View(inventory);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSettings(InventoryDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (dto.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();

            var command = new UpdateInventoryCommand
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                IsPublic = dto.IsPublic,
                CategoryId = dto.CategoryId,
                Version = dto.Version
            };

            try
            {
                await _mediator.Send(command);
            }
            catch(Inventra.Application.Common.Exceptions.ConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Someone else modified this inventory. Please reload and try again");
                return View("Edit", dto);
            }
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFields(InventoryDto dto)
        {
            var existing = await _mediator.Send(new GetInventoryByIdQuery(dto.Id));
            if (existing == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (existing.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();

            dto.Title = existing.Title;
            dto.Version = existing.Version;
            dto.CategoryId = existing.CategoryId;

            var command = new UpdateInventoryCommand
            {
                Id = dto.Id,
                Title = dto.Title ?? string.Empty,
                Version = dto.Version,
                CustomString1Name = dto.CustomString1Name,
                CustomString2Name = dto.CustomString2Name,
                CustomString3Name = dto.CustomString3Name,
                CustomInt1Name = dto.CustomInt1Name,
                CustomInt2Name = dto.CustomInt2Name,
                CustomInt3Name = dto.CustomInt3Name,
                CustomText1Name = dto.CustomText1Name,
                CustomText2Name = dto.CustomText2Name,
                CustomText3Name = dto.CustomText3Name,
                CustomBool1Name = dto.CustomBool1Name,
                CustomBool2Name = dto.CustomBool2Name,
                CustomBool3Name = dto.CustomBool3Name,
                CustomLink1Name = dto.CustomLink1Name,
                CustomLink2Name = dto.CustomLink2Name,
                CustomLink3Name = dto.CustomLink3Name
            };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Details), new {id = dto.Id});
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
