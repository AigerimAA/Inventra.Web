using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Inventra.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(int inventoryId, string message)
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var timestamp = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm");

            await Clients.Group(inventoryId.ToString())
                .SendAsync("ReceiveMessage", userName, message, timestamp);
        }
        public async Task JoinInventory(int inventoryId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, inventoryId.ToString());
        }
    }
}
