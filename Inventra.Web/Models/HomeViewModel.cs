using Inventra.Application.DTOs;

namespace Inventra.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<InventoryDto> LatestInventories { get; set; } = Enumerable.Empty<InventoryDto>();
        public IEnumerable<InventoryDto> PopularInventories { get; set; } = Enumerable.Empty<InventoryDto>();
        public IEnumerable<TagCloudItemDto> TagCloud { get; set; } = Enumerable.Empty<TagCloudItemDto>();
    }
}

