using Microsoft.AspNetCore.SignalR;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Chat
{
    public class SignalRChatNotifier : IChatNotifier
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalRChatNotifier(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewMessageAsync(int receiverId, ChatMessageDto message)
        {
            await _hubContext.Clients
             .Group($"user-{receiverId}")
             .SendAsync("ReceiveMessage", message);
        }
        public async Task NotifyMessagesReadAsync(int notifyUserId, int readByUserId)
        {
            await _hubContext.Clients.Group($"user-{notifyUserId}")
                .SendAsync("MessagesRead", new { readByUserId });
        }
    }
}
