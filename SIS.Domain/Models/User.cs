using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain.Models
{
    public class User:BaseEntity
    {
        public string Email { get; set; } = string.Empty;
       
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
    }
}
