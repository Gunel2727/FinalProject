using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class AuthDto
    {
        
        public class LoginDto
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }


        public class ChangePasswordDto
        {
            public string OldPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }


        public class AuthResponseDto
        {
            public string Token { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public int UserId { get; set; }
            public int? StudentId { get; set; }
            public int? TeacherId { get; set; }
            public bool MustChangePassword { get; set; }
        }

        public class GoogleLoginDto
        {
            public string IdToken { get; set; } = string.Empty;
        }
    }
}
