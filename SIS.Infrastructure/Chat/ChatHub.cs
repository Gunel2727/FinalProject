using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<int, int> _connectionCounts = new();
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("userId")?.Value;

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out var uid))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{uid}");
                var newCount = _connectionCounts.AddOrUpdate(uid, 1, (_, count) => count + 1);
                if (newCount == 1)
                    await Clients.All.SendAsync("UserOnline", uid);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("userId")?.Value;

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out var uid))
            {
                var newCount = _connectionCounts.AddOrUpdate(uid, 0, (_, count) => System.Math.Max(0, count - 1));
                if (newCount == 0)
                {
                    _connectionCounts.TryRemove(uid, out _);
                    await Clients.All.SendAsync("UserOffline", uid);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public Task<int[]> GetOnlineUserIds()
        {
            return Task.FromResult(_connectionCounts.Keys.ToArray());
        }
    }
}
