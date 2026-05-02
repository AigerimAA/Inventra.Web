using Inventra.Domain.Interfaces;
using Inventra.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<IActionResult> Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View(new SearchResultViewModel());

            ViewData["SearchQuery"] = q;

            var inventories = await _searchRepository.SearchInventoriesAsync(q);
            var items = await _searchRepository.SearchItemsAsync(q);

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
