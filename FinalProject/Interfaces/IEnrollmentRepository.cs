using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IList<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);
        Task AddAsync(Enrollment enrollment);
        void Delete(Enrollment enrollment);
    }
}
