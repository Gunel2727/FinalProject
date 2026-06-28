using SIS.Domain.Interfaces;
using StudentInformationSystem.Domain.Models;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IAttendanceRepository:IGenericRepository<Attendance>
    {
        Task<IList<Attendance>> GetByStudentIdAsync(int studentId);
        Task<IList<Attendance>> GetByCourseIdAsync(int courseId);
        Task<IList<Attendance>> GetByCourseAndDateAsync(int courseId, DateTime date);
        void Update(Attendance attendance);
    }
}
