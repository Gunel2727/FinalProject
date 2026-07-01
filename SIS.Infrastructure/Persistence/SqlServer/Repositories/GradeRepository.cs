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

        public void Update(Grade grade)
        {
            _context.Grades.Update(grade);
        }
    }
}
