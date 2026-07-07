using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.DTOs
{
    public class StudentDashboardDto
    {
        public string FullName { get; set; } = string.Empty;
        public string ProgrammeName { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public double Gpa { get; set; }
        public List<CourseDto> Courses { get; set; } = new();
        public List<AnnouncementDto> RecentAnnouncements { get; set; } = new();
    }
    public class TeacherDashboardDto
    {
        public string FullName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public List<CourseDto> Courses { get; set; } = new();
        public List<AnnouncementDto> RecentAnnouncements { get; set; } = new();
    }
    public class AdminDashboardDto
    {
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalDepartments { get; set; }
        public List<AnnouncementDto> RecentAnnouncements { get; set; } = new();
    }

    public class AdvisorStudentOverviewDto
    {
        public string FullName { get; set; } = string.Empty;
        public string ProgrammeName { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public double Gpa { get; set; }
        public List<GradeDto> Grades { get; set; } = new();
        public int TotalAttendanceRecords { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
    }
}
