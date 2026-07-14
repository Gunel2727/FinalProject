using Microsoft.EntityFrameworkCore;
using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public  void Update(Student student)
        {
            _context.Students.Update(student);
        }
        public new async Task<IList<Student>> GetAllAsync()
        {
            return await _context.Students
           .Include(s => s.Programme)
            .ThenInclude(p => p.Department)
           .ToListAsync();
        }

        
        public new async Task<Student?> GetByIdAsync(int id)
            => await _context.Students
                .Include(s => s.Programme)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Student?> GetByEmailAsync(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<IList<Student>> GetFilteredAsync(string? search, int? programmeId,int? departmenId)
        {
            var query = _context.Students
                .Include(s => s.Programme)
                .AsQueryable();

            if(!string.IsNullOrEmpty(search))
            {
                query=query.Where(s => s.FirstName.Contains(search) 
                || s.LastName.Contains(search)
                || s.Email.Contains(search));
            }

            if (programmeId.HasValue)
            {
                query = query.Where(s => s.ProgrammeId == programmeId.Value);
            }

            if(departmenId.HasValue) {
                query = query.Where(s => s.Programme.DepartmentId == departmenId.Value);
            }

            return await query.ToListAsync();

        }
    }
}
