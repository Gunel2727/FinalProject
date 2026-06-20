using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IList<Announcement>> GetAllAsync();
        Task<IList<Announcement>> GetByCourseIdAsync(int courseId);
        Task AddAsync(Announcement announcement);
        void Delete(Announcement announcement);
    }
}
