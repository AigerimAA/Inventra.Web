using Inventra.Application.DTOs;

namespace Inventra.Web.Models
{
    public class ProfileViewModel
    {
        public IEnumerable<InventoryDto> OwnedInventories { get; set; } = Enumerable.Empty<InventoryDto>();
        public IEnumerable<InventoryDto> AccessibleInventories { get; set; } = Enumerable.Empty<InventoryDto>();
    }
}
