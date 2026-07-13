using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IGradeRepository:IGenericRepository<Grade>
    {
        Task<IList<Grade>> GetByStudentIdAsync(int studentId);
        Task<IList<Grade>> GetByCourseIdAsync(int courseId);
        Task<Grade?> GetByStudentAndCourseAsync(int studentId, int courseId);
        void Update(Grade grade);
        Task<IList<Grade>> GetFilteredAsync(string? search, int? courseId);
    }
}
