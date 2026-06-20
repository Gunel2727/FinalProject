using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IList<Attendance>> GetByStudentIdAsync(int studentId);
        Task<IList<Attendance>> GetByCourseIdAsync(int courseId);
        Task<IList<Attendance>> GetByCourseAndDateAsync(int courseId, DateTime date);
        Task AddAsync(Attendance attendance);
        void Update(Attendance attendance);
    }
}
