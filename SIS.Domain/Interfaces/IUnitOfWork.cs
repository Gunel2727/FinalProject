using SIS.Domain.Interfaces;

namespace StudentInformationSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ITeacherRepository Teachers { get; }
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        IGradeRepository Grades { get; }
        IAttendanceRepository Attendances { get; }
        IAnnouncementRepository Announcements { get; }
        IDepartmentRepository Departments { get; }
        IProgrammeRepository Programmes { get; }
        IAcademicTermRepository AcademicTerms { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
