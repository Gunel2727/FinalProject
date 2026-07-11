using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SIS.Api.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId=Context.User?.FindFirst("userId")?.Value;

            if(!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");
            }
            await base.OnConnectedAsync();
        }
    }
}
