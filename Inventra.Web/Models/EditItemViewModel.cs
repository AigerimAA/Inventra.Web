using Inventra.Application.DTOs;
using Inventra.Application.Items.Commands.UpdateItem;

namespace Inventra.Web.Models
{
    public class EditItemViewModel
    {
        public UpdateItemCommand Command { get; set; } = new();
        public InventoryDto Inventory { get; set; } = new();
    }
}
