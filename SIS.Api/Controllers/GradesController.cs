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
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly ITranscriptService _transcriptService;

        public GradesController(IGradeService gradeService, ITranscriptService transcriptService)
        {
            _gradeService = gradeService;
            _transcriptService = transcriptService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var grades = await _gradeService.GetByStudentIdAsync(studentId);
            return Ok(ResponseModel<IList<GradeDto>>.Ok(grades));
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var grades = await _gradeService.GetByCourseIdAsync(courseId);
            return Ok(ResponseModel<IList<GradeDto>>.Ok(grades));
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            var grade = await _gradeService.CreateAsync(dto);
            return StatusCode(201, ResponseModel<GradeDto>.Created(grade));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGradeDto dto)
        {
            var grade = await _gradeService.UpdateAsync(id, dto);
            return Ok(ResponseModel<GradeDto>.Ok(grade));
        }

        [HttpGet("student/{studentId}/transcript")]
        public async Task<IActionResult> DownloadTranscript(int studentId)
        {
            var pdfBytes = await _transcriptService.GenerateTranscriptAsync(studentId);

            
            return File(
                pdfBytes,
                "application/pdf",
                $"transcript_{studentId}.pdf"
            );
        }
    }
}
