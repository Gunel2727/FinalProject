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
    public class ProgrammeRepository : GenericRepository<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Programme programme)
        {
            _context.Programmes.Remove(programme);
        }

        public void Update(Programme programme)
        {
            _context.Programmes.Update(programme);
        }

        public new async Task<IList<Programme>> GetAllAsync()
        {
            return await _context.Programmes
                .Include(p => p.Department)
                .ToListAsync();
        }

        public new async Task<Programme?> GetByIdAsync(int id)
        { 
            return await _context.Programmes
            .Include(p => p.Department)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Programme>> GetFilteredAsync(string? search, int? departmentId)
        {
            var query = _context.Programmes
                .Include(p => p.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(p => p.DepartmentId == departmentId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
