using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Interfaces
{
    public interface IAiChatService
    {
        Task<string> AskAsync(string userMessage);
    }
}
