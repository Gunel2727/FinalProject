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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
        }

        public async Task<IList<Department>> GetFilteredAsync(string? search)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }
    }
}
