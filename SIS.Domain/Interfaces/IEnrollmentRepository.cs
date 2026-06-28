using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IEnrollmentRepository:IGenericRepository<Enrollment>
    {
        Task<IList<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);
        void Delete(Enrollment enrollment);
    }
}
