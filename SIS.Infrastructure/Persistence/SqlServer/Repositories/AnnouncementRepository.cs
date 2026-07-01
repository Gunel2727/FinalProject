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
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Announcement announcement)
        {
            _context.Announcements.Remove(announcement);
        }

        public async Task<IList<Announcement>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Announcements
            .Where(a => a.CourseId == courseId || a.IsGlobal)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
        }
    }
}
