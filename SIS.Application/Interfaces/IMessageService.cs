using SIS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IMessageService
    {
        Task<ChatMessageDto> SendMessageAsync(int senderId,SendMessageDto dto);
        Task<IList<ChatMessageDto>> GetConversationAsync(int userId1, int userId2);
        Task MarkAsReadAsync(int currentUserId, int otherUserId);
    }
}
