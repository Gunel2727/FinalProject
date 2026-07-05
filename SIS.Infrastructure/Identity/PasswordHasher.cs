using Microsoft.AspNetCore.Identity;
using SIS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
           return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
