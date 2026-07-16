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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var enrollments = await _enrollmentService.GetByStudentIdAsync(studentId);
            return Ok(ResponseModel<IList<EnrollmentDto>>.Ok(enrollments));
        }



        [HttpPost]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Enroll([FromBody] CreateEnrollmentDto dto)
        {
            var enrollment = await _enrollmentService.EnrollAsync(dto);
            return StatusCode(201, ResponseModel<EnrollmentDto>.Created(enrollment));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Unenroll(int id)
        {
            await _enrollmentService.UnenrollAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }
    }
}
