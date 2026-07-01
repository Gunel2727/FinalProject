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
    public class CourseRepository:GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }
        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<IList<Course>> GetByTeacherIdAsync(int teacherId)
        {
           return await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.AcademicTerm)
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
        public new async Task<IList<Course>> GetAllAsync()
        {
           return await _context.Courses
           .Include(c => c.Teacher)
           .Include(c => c.AcademicTerm)
           .ToListAsync();

        }
        

    }
}
