using Inventra.Application.Categories.Queries.GetAllCategories;
using Inventra.Application.DTOs;
using Inventra.Application.Interfaces;
using Inventra.Application.Inventories.Commands.CreateInventory;
using Inventra.Application.Inventories.Commands.DeleteInventory;
using Inventra.Application.Inventories.Commands.UpdateInventory;
using Inventra.Application.Inventories.Queries.GetAllInventories;
using Inventra.Application.Inventories.Queries.GetInventoryById;
using Inventra.Application.Items.Queries.GetItemsByInventoryId;
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
        private readonly IInventoryPermissionService _permissionService;
        private readonly ICurrentUserService _currentUserService;

        public InventoryController(IMediator mediator, UserManager<ApplicationUser> userManager,
            IInventoryPermissionService permissionService, ICurrentUserService currentService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _permissionService = permissionService;
            _currentUserService = currentService;
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _mediator.Send(new GetAllInventoriesQuery());
            return View(inventories);
        }
        public async Task<IActionResult> Details(int id)
        {
            var inventory = await _mediator.Send(new GetInventoryByIdQuery(id));
            if (inventory == null) return NotFound();

            var items = await _mediator.Send(new GetItemsByInventoryIdQuery(id));
            ViewBag.Items = items;

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
            var commandWithOwner = command with { OwnerId = userId };

            var result = await _mediator.Send(commandWithOwner);
            return RedirectToAction(nameof(Details), new { id = result.Id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _currentUserService.UserId
                ?? return Forbid();

            if (!await _permissionService.CanManageAsync(
                    userId, _currentUserService.IsAdmin, id))
                return Forbid();

            var inventory = await _mediator.Send(new GetInventoryByIdQuery(id));
            if (inventory == null) return NotFound();

            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering
                .SelectList(categories, "Id", "Name");

            return View(inventory);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSettings(InventoryDto dto)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return Forbid();

            if (!await _permissionService.CanManageAsync(
                    userId, _currentUserService.IsAdmin, dto.Id))
                return Forbid();

            var command = new UpdateInventoryCommand
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                IsPublic = dto.IsPublic,
                CategoryId = dto.CategoryId,
                Tags = dto.Tags
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
            var userId = _currentUserService.UserId;
            if (userId == null) return Forbid();

            if (!await _permissionService.CanManageAsync(
                    userId, _currentUserService.IsAdmin, dto.Id))
                return Forbid();

            var existing = await _mediator.Send(new GetInventoryByIdQuery(dto.Id));
            if (existing == null) return NotFound();

            var command = new UpdateInventoryCommand
            {
                Id = dto.Id,
                Title = existing.Title,
                CategoryId = existing.CategoryId,
                Tags = existing.Tags,

                CustomString1Name = dto.CustomString1Name,
                CustomString1Shown = dto.CustomString1Shown,
                CustomString2Name = dto.CustomString2Name,
                CustomString2Shown = dto.CustomString2Shown,
                CustomString3Name = dto.CustomString3Name,
                CustomString3Shown = dto.CustomString3Shown,

                CustomInt1Name = dto.CustomInt1Name,
                CustomInt1Shown = dto.CustomInt1Shown,
                CustomInt2Name = dto.CustomInt2Name,
                CustomInt2Shown = dto.CustomInt2Shown,
                CustomInt3Name = dto.CustomInt3Name,
                CustomInt3Shown = dto.CustomInt3Shown,

                CustomText1Name = dto.CustomText1Name,
                CustomText1Shown = dto.CustomText1Shown,
                CustomText2Name = dto.CustomText2Name,
                CustomText2Shown = dto.CustomText2Shown,
                CustomText3Name = dto.CustomText3Name,
                CustomText3Shown = dto.CustomText3Shown,

                CustomBool1Name = dto.CustomBool1Name,
                CustomBool1Shown = dto.CustomBool1Shown,
                CustomBool2Name = dto.CustomBool2Name,
                CustomBool2Shown = dto.CustomBool2Shown,
                CustomBool3Name = dto.CustomBool3Name,
                CustomBool3Shown = dto.CustomBool3Shown,

                CustomLink1Name = dto.CustomLink1Name,
                CustomLink1Shown = dto.CustomLink1Shown,
                CustomLink2Name = dto.CustomLink2Name,
                CustomLink2Shown = dto.CustomLink2Shown,
                CustomLink3Name = dto.CustomLink3Name,
                CustomLink3Shown = dto.CustomLink3Shown
            };

            await _mediator.Send(command);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
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
