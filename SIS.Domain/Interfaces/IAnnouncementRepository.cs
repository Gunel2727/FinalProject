using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAnnouncementRepository:IGenericRepository<Announcement>
    {
        Task<IList<Announcement>> GetByCourseIdAsync(int courseId);
        void Delete(Announcement announcement);
        Task<IList<Announcement>> GetFilteredAsync(string? targetRole, int? courseId);
        Task<IList<Announcement>> GetVisibleForRoleAsync(string? role);
    }
}
