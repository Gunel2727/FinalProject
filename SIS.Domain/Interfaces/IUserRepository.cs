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
    }
}
