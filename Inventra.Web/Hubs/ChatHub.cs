using Inventra.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Inventra.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(int inventoryId, string message)
        {
            var user = await _userManager.GetUserAsync(Context.User!);
            if (user == null || user.IsBlocked)
            {
                await Clients.Caller.SendAsync("Error", "You are not allowed to send messages");
                return;
            }

            var timestamp = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm");

            await Clients.Group(inventoryId.ToString())
                .SendAsync("ReceiveMessage", user.UserName, message, timestamp);
        }

        public async Task JoinInventory(int inventoryId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, inventoryId.ToString());
        }
    }
}
