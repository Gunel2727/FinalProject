using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class AiChatDto
    {
        public class AiChatRequestDto
        {
            public string Message { get; set; } = string.Empty;
        }

        public class AiChatResponseDto
        {
            public string Reply { get; set; } = string.Empty;
        }
    }
}
