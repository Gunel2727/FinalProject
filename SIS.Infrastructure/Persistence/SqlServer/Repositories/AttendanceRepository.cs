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
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Attendance>> GetByCourseAndDateAsync(int courseId, DateTime date)
        {
            return await _context.Attendances
                .Where(a => a.CourseId == courseId &&
                        a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IList<Attendance>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Course)
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IList<Attendance>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Course)
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public void Update(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
        }
    }
}
