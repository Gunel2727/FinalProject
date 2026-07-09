using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
        }

        public async Task<Teacher?> GetByEmailAsync(string email)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        }

        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
        }
    }
}
