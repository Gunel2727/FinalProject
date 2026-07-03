using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var grades = await _gradeService.GetByStudentIdAsync(studentId);
            return Ok(ResponseModel<IList<GradeDto>>.Ok(grades));
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var grades = await _gradeService.GetByCourseIdAsync(courseId);
            return Ok(ResponseModel<IList<GradeDto>>.Ok(grades));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            var grade = await _gradeService.CreateAsync(dto);
            return StatusCode(201, ResponseModel<GradeDto>.Created(grade));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGradeDto dto)
        {
            var grade = await _gradeService.UpdateAsync(id, dto);
            return Ok(ResponseModel<GradeDto>.Ok(grade));
        }   
    }
}
