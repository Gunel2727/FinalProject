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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
        public async Task<bool> EmailExistsAsync(string email)
        {
          return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> TeacherIdExistsAsync(int teacherId)
        {
           return await _context.Users.AnyAsync(u => u.TeacherId == teacherId);
        }


        public async Task<bool> StudentIdExistsAsync(int studentId)
        {
           return await _context.Users.AnyAsync(u => u.StudentId == studentId);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
