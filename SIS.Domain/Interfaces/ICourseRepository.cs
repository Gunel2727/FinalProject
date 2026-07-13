using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface ICourseRepository:IGenericRepository<Course>
    {
        Task<IList<Course>> GetByTeacherIdAsync(int teacherId);
        void Update(Course course);
        void Delete(Course course);
        Task<IList<Course>> GetFilteredAsync(string? search,int? teacherId,int? academicTermId);
    }
}
