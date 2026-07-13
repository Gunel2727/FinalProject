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
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Grade>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Grades
                .Include(x => x.Student)
                .Where(g => g.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<Grade?> GetByStudentAndCourseAsync(int studentId, int courseId)
        {
            return await _context.Grades
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.CourseId == courseId);
        }

        public async Task<IList<Grade>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Grades
                .Include(x => x.Course)
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IList<Grade>> GetFilteredAsync(string? search, int? courseId)
        {
           var query = _context.Grades
                .Include(x => x.Student)
                .Include(x => x.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(g => g.Student.FirstName.Contains(search) || g.Student.LastName.Contains(search));
            }

            if (courseId.HasValue)
            {
                query = query.Where(g => g.CourseId == courseId.Value);
            }

            return await query.ToListAsync();
        }

        public void Update(Grade grade)
        {
            _context.Grades.Update(grade);
        }
    }
}
