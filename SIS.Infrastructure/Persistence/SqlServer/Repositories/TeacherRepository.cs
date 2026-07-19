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

        public async Task<IList<Teacher>> GetFilteredAsync(string? search, int? departmentId)
        {
            var query = _context.Teachers
                .Include(t => t.Department)
                .AsQueryable();

            if(!string.IsNullOrEmpty(search))
            {
                query=query.Where(t=>t.FirstName.Contains(search)
                || t.LastName.Contains(search)
                || t.Email.Contains(search)
                );
            }

            if(departmentId.HasValue)
            {
                query=query.Where(t=>t.DepartmentId==departmentId.Value);
            }

            return await query.ToListAsync();
        }

        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
        }

        public new async Task<IList<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include (t => t.Department)
                .ToListAsync();
        }
    }
}
