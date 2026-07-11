using SIS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain.Interfaces
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        Task <IList<ChatMessage>> GetConversationAsync(int userId1, int userId2);
    }
}
