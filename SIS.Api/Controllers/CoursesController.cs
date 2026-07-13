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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(ResponseModel<IList<CourseDto>>.Ok(courses));
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] string? search,  [FromQuery] int? teacherId, [FromQuery] int? academicTermId)
        {
           var courses = await _courseService.GetFilteredAsync(search, teacherId, academicTermId);
            return Ok(ResponseModel<IList<CourseDto>>.Ok(courses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return Ok(ResponseModel<CourseDto>.Ok(course));
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {
            var courses = await _courseService.GetByTeacherIdAsync(teacherId);
            return Ok(ResponseModel<IList<CourseDto>>.Ok(courses));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var course =await _courseService.CreateAsync(dto);
            return StatusCode(201, ResponseModel<CourseDto>.Created(course));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto dto)
        {
            var course = await _courseService.UpdateAsync(id, dto);
            return Ok(ResponseModel<CourseDto>.Ok(course));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }

    }
}
