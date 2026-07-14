using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentDashboard(int studentId)
        {
            var dashboard = await _dashboardService.GetStudentDashboardAsync(studentId);
            return Ok(ResponseModel<StudentDashboardDto>.Ok(dashboard));
        }


        [HttpGet("chart/gpa-progress/{studentId}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> GetGpaProgress(int studentId)
        {
            var data = await _dashboardService.GetGpaProgressAsync(studentId);
            return Ok(ResponseModel<IList<SemesterGpaDto>>.Ok(data));
        }


        [HttpGet("teacher/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetTeacherDashboard(int teacherId)
        {
            var dashboard = await _dashboardService.GetTeacherDashboardAsync(teacherId);
            return Ok(ResponseModel<TeacherDashboardDto>.Ok(dashboard));
        }


        [HttpGet("chart/average-grade-per-course/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetAverageGradePerCourse(int teacherId)
        {
            var data = await _dashboardService.GetAverageGradePerCourseAsync(teacherId);
            return Ok(ResponseModel<IList<CourseAverageDto>>.Ok(data));
        }


        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var dashboard = await _dashboardService.GetAdminDashboardAsync();
            return Ok(ResponseModel<AdminDashboardDto>.Ok(dashboard));
        }

        [HttpGet("chart/students-by-programme")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentsByProgramme()
        {
            var chartData = await _dashboardService.GetStudentsByProgrammeAsync();
            return Ok(ResponseModel<IList<ChartDataPointDto>>.Ok(chartData));
        }

        [HttpGet("chart/students-by-department")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentsByDepartment()
        {
            var chartData = await _dashboardService.GetStudentsByDepartmentAsync();
            return Ok(ResponseModel<IList<ChartDataPointDto>>.Ok(chartData));
        }

        [HttpGet("chart/grade-distribution")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetGradeDistribution()
        {
            var chartData = await _dashboardService.GetGradeDistributionAsync();
            return Ok(ResponseModel<IList<ChartDataPointDto>>.Ok(chartData));
        }

        [HttpGet("advisor/{studentId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetAdvisorOverview(int studentId)
        {
            var overview = await _dashboardService.GetAdvisorOverviewAsync(studentId);
            return Ok(ResponseModel<AdvisorStudentOverviewDto>.Ok(overview));
        }

    }
}
