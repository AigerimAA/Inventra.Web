using Inventra.Application.DTOs;

namespace Inventra.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<InventoryDto> LatestInventories { get; set; } = [];
        public IEnumerable<InventoryDto> PopularInventories { get; set; } = [];
    }
}
