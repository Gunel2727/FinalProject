using AutoMapper;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
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
