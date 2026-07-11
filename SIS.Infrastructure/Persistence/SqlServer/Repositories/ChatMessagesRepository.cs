using Microsoft.EntityFrameworkCore;
using SIS.Domain.Interfaces;
using SIS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class ChatMessagesRepository : GenericRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessagesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<ChatMessage>> GetConversationAsync(int userId1, int userId2)
        {
            return await _context.ChatMessages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2)
                || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }
    }
}
