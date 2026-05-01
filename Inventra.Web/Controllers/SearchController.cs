using FluentValidation.TestHelper;
using Inventra.Domain.Entities;
using Inventra.Infrastructure.Persistence;
using Inventra.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View(new SearchResultViewModel());

            ViewData["SearchQuery"] = q;

            var inventories = await _context.Inventories
                .Include(i => i.Owner)
                .Include(i => i.Category)
                .Include(i => i.Items)
                .Where(i => i.Title.Contains(q) ||
                        (i.Description != null && i.Description.Contains(q))).ToListAsync();

            var items = await _context.Items
                .Include(i => i.Inventory)
                .Include(i => i.CreatedBy)
                .Where(i => i.CustomId.Contains(q) ||
                        (i.CustomString1Value != null && i.CustomString1Value.Contains(q)) ||
                        (i.CustomString2Value != null && i.CustomString2Value.Contains(q)) ||
                        (i.CustomString3Value != null && i.CustomString3Value.Contains(q)))
                .ToListAsync();

            var result = new SearchResultViewModel
            {
                Query = q,
                Inventories = inventories.Select(i => new SearchInventoryResult
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    CategoryName = i.Category?.Name,
                    OwnerName = i.Owner?.UserName,
                    ItemsCount = i.Items.Count
                }).ToList(),
                Items = items.Select(i => new SearchItemResult
                {
                    Id = i.Id,
                    CustomId = i.CustomId,
                    InventoryId = i.InventoryId,
                    InventoryTitle = i.Inventory?.Title,
                    CreatedByName = i.CreatedBy?.UserName
                }).ToList()
            };
            return View(result);
        }
    }
}
