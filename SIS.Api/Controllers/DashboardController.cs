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

        [HttpGet("teacher/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetTeacherDashboard(int teacherId)
        {
            var dashboard = await _dashboardService.GetTeacherDashboardAsync(teacherId);
            return Ok(ResponseModel<TeacherDashboardDto>.Ok(dashboard));
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var dashboard = await _dashboardService.GetAdminDashboardAsync();
            return Ok(ResponseModel<AdminDashboardDto>.Ok(dashboard));
        }

    }
}
