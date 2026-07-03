using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var records = await _attendanceService.GetByStudentIdAsync(studentId);
            return Ok(ResponseModel<IList<AttendanceDto>>.Ok(records));
        }

        [HttpGet("course/{courseId}")] 
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var records = await _attendanceService.GetByCourseIdAsync(courseId);
            return Ok(ResponseModel<IList<AttendanceDto>>.Ok(records));
        }

        [HttpPost]
        public async Task<IActionResult> Record([FromBody] CreateAttendanceDto dto)
        {
            var record = await _attendanceService.RecordAsync(dto);
            return StatusCode(201, ResponseModel<AttendanceDto>.Created(record));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAttendanceDto dto)
        {
            var record = await _attendanceService.UpdateAsync(id, dto);
            return Ok(ResponseModel<AttendanceDto>.Ok(record));
        }
    }
}
