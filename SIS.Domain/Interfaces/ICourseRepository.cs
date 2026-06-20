using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course?> GetByIdAsync(int id);
        Task<IList<Course>> GetAllAsync();
        Task<IList<Course>> GetByTeacherIdAsync(int teacherId);
        Task AddAsync(Course course);
        void Update(Course course);
        void Delete(Course course);
    }
}
