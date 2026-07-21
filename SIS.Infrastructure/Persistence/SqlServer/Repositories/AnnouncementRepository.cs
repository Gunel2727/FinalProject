using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Domain.Enums;
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

        public async Task<IList<Announcement>> GetFilteredAsync(string? targetRole, int? courseId)
        {
            var query = _context.Announcements.AsQueryable();

            if (!string.IsNullOrEmpty(targetRole))
            {
                var role = Enum.Parse<UserRole>(targetRole, ignoreCase: true);
                query = query.Where(a => a.IsGlobal || a.TargetRole == null || a.TargetRole == role);
            }

            if (courseId.HasValue)
            {
                query = query.Where(a => a.CourseId == courseId.Value || a.IsGlobal);
            }

            return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
        }

        public async Task<IList<Announcement>> GetVisibleForRoleAsync(string? role)
        {
            var query = _context.Announcements.AsQueryable();

            if (!string.IsNullOrEmpty(role)
                && !role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                && Enum.TryParse<UserRole>(role, true, out var userRole))
            {
                query = query.Where(a => a.IsGlobal || a.TargetRole == null || a.TargetRole == userRole);
            }

            return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
        }
    }
}
