using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using StudentInformationSystem.Domain.Enums;
using StudentInformationSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGpaCalculatorService _gpaCalculator;

        public DashboardService(
            IUnitOfWork uow,
            IMapper mapper,
            IGpaCalculatorService gpaCalculator)
        {
            _uow = uow;
            _mapper = mapper;
            _gpaCalculator = gpaCalculator;
        }
        public async Task<AdminDashboardDto> GetAdminDashboardAsync()
        {
            
            var students = await _uow.Students.GetAllAsync();
            var teachers = await _uow.Teachers.GetAllAsync();
            var courses = await _uow.Courses.GetAllAsync();
            var departments = await _uow.Departments.GetAllAsync();
            var announcements = await _uow.Announcements.GetAllAsync();
            var recent = announcements.OrderByDescending(a => a.CreatedAt).Take(5);

            return new AdminDashboardDto
            {
                TotalStudents = students.Count(),
                TotalTeachers = teachers.Count(),
                TotalCourses = courses.Count(),
                TotalDepartments = departments.Count(),
                RecentAnnouncements = _mapper.Map<List<AnnouncementDto>>(recent)
            };
        }

        public async Task<AdvisorStudentOverviewDto> GetAdvisorOverviewAsync(int studentId)
        {
            var student = await _uow.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

            var grades = await _uow.Grades.GetByStudentIdAsync(studentId);
            var gradesList = grades.ToList();
            var scores = gradesList.Select(g => g.Score).ToList(); 

            var attendance = await _uow.Attendances.GetByStudentIdAsync(studentId);
            var attendanceList = attendance.ToList();

            return new AdvisorStudentOverviewDto
            {
                FullName = $"{student.FirstName} {student.LastName}",
                ProgrammeName = student.Programme?.Name ?? string.Empty,
                AcademicYear = student.AcademicYear,
                Gpa = _gpaCalculator.Calculate(scores),
                Grades = _mapper.Map<List<GradeDto>>(gradesList),
                TotalAttendanceRecords = attendanceList.Count,
                PresentCount = attendanceList.Count(a => a.Status == AttendanceStatus.Present),
                AbsentCount = attendanceList.Count(a => a.Status == AttendanceStatus.Absent),
                LateCount = attendanceList.Count(a => a.Status == AttendanceStatus.Late)
            };
        }

        public async Task<IList<CourseAverageDto>> GetAverageGradePerCourseAsync(int teacherId)
        {
            var courses= await _uow.Courses.GetByTeacherIdAsync(teacherId);
            var result = new List<CourseAverageDto>();

            foreach (var course in courses)
            {
                var grades = await _uow.Grades.GetByCourseIdAsync(course.Id);
                var scores = grades.Select(g => g.Score).ToList();
                var average = scores.Count > 0 ? scores.Average() : 0;

                result.Add(new CourseAverageDto
                {
                    CourseName = course.Name,
                    AverageScore = Math.Round(average, 2)  
                });

            }
            return result;
                
        }

        public async Task<IList<SemesterGpaDto>> GetGpaProgressAsync(int studentId)
        {
           var grades = await _uow.Grades.GetByStudentIdAsync(studentId);
            var grouped = grades.GroupBy(g => g.Course.AcademicTerm.Name)
                     .Select(g => new SemesterGpaDto
                     {
                         SemesterName = g.Key,
                         Gpa = _gpaCalculator.Calculate(g.Select(x => x.Score).ToList())
                     })
                     .ToList();
            return grouped;
        }

        public async Task<IList<ChartDataPointDto>> GetGradeDistributionAsync()
        {
            var grades= await _uow.Grades.GetAllAsync();
            var grouped = grades.GroupBy(g => g.Letter.ToString())
                .Select(g => new ChartDataPointDto
                {
                    Label = g.Key,
                    Count = g.Count()
                })
                .ToList();
            return grouped;
        }

        public async Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId)
        {
            var student = await _uow.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new NotFoundException(ErrorMessages.StudentNotFound);

           
            var grades = await _uow.Grades.GetByStudentIdAsync(studentId);
            var scores = grades.Select(g => g.Score).ToList();


            var enrollments = await _uow.Enrollments.GetByStudentIdAsync(studentId);
            var courses = enrollments.Select(e => e.Course);

            
            var announcements = await _uow.Announcements.GetAllAsync();
            var recent = announcements.OrderByDescending(a => a.CreatedAt).Take(5);

            return new StudentDashboardDto
            {
                FullName = $"{student.FirstName} {student.LastName}",
                ProgrammeName = student.Programme?.Name ?? string.Empty,
                AcademicYear = student.AcademicYear,
                
                Gpa = _gpaCalculator.Calculate(scores),
                Courses = _mapper.Map<List<CourseDto>>(courses),
                RecentAnnouncements = _mapper.Map<List<AnnouncementDto>>(recent)
            };
        }

        public async Task<IList<ChartDataPointDto>> GetStudentsByDepartmentAsync()
        {
            var students = await _uow.Students.GetAllAsync();
            var grouped=students.GroupBy(s=>s.Programme.Department.Name)
                .Select(g => new ChartDataPointDto
                {
                    Label=g.Key,
                    Count=g.Count()
                })
                .ToList();
            return grouped;
        }

        public async Task<IList<ChartDataPointDto>> GetStudentsByProgrammeAsync()
        {
            var students = await _uow.Students.GetAllAsync();
            var grouped=students.GroupBy(s=>s.Programme.Name)
                .Select(g=>new ChartDataPointDto
                {
                    Label = g.Key,
                    Count = g.Count()
                })
                .ToList();
            return grouped;
        }

        public async Task<TeacherDashboardDto> GetTeacherDashboardAsync(int teacherId)
        {
            var teacher = await _uow.Teachers.GetByIdAsync(teacherId);
            if (teacher == null)
                throw new NotFoundException(ErrorMessages.TeacherNotFound);

            var courses = await _uow.Courses.GetByTeacherIdAsync(teacherId);
            var announcements = await _uow.Announcements.GetAllAsync();
            var recent = announcements.OrderByDescending(a => a.CreatedAt).Take(5);

            return new TeacherDashboardDto
            {
                FullName = $"{teacher.FirstName} {teacher.LastName}",
                DepartmentName = teacher.Department?.Name ?? string.Empty,
                Courses = _mapper.Map<List<CourseDto>>(courses),
                RecentAnnouncements = _mapper.Map<List<AnnouncementDto>>(recent)
            };
        }
    }
}
