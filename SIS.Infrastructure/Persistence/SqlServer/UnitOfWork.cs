using SIS.Infrastructure.Persistence.SqlServer.Repositories;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Persistence.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;


        private IStudentRepository? _students;
        private ITeacherRepository? _teachers;
        private ICourseRepository? _courses;
        private IEnrollmentRepository? _enrollments;
        private IGradeRepository? _grades;
        private IAttendanceRepository? _attendances;
        private IAnnouncementRepository? _announcements;
        private IDepartmentRepository? _departments;
        private IProgrammeRepository? _programmes;
        private IAcademicTermRepository? _academicTerms;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students =>
       _students ??= new StudentRepository(_context);

        public ITeacherRepository Teachers =>
            _teachers ??= new TeacherRepository(_context);

        public ICourseRepository Courses =>
            _courses ??= new CourseRepository(_context);

        public IEnrollmentRepository Enrollments =>
            _enrollments ??= new EnrollmentRepository(_context);

        public IGradeRepository Grades =>
            _grades ??= new GradeRepository(_context);

        public IAttendanceRepository Attendances =>
            _attendances ??= new AttendanceRepository(_context);

        public IAnnouncementRepository Announcements =>
            _announcements ??= new AnnouncementRepository(_context);

        public IDepartmentRepository Departments =>
            _departments ??= new DepartmentRepository(_context);

        public IProgrammeRepository Programmes =>
            _programmes ??= new ProgrammeRepository(_context);

        public IAcademicTermRepository AcademicTerms =>
            _academicTerms ??= new AcademicTermRepository(_context);
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
