using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IGradeRepository
    {
        
        Task<IList<Grade>> GetByStudentIdAsync(int studentId);
        Task<IList<Grade>> GetByCourseIdAsync(int courseId);
        Task<Grade?> GetByStudentAndCourseAsync(int studentId, int courseId);
        Task AddAsync(Grade grade);
        void Update(Grade grade);
    }
}
