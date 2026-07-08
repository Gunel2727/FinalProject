using SIS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        
        Task<User?> GetByEmailAsync(string email);
      
        Task<bool> EmailExistsAsync(string email);
        Task<bool> TeacherIdExistsAsync(int teacherId);
        Task<bool> StudentIdExistsAsync(int studentId);
        void Update(User user);
    }
}
