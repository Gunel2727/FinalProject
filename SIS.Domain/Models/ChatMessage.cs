using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain.Models
{
    public class ChatMessage:BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Content { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public User Sender { get; set; }=null!;
        public User Receiver { get; set; }= null!;
    }
}
