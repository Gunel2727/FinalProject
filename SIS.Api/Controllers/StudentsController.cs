using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

       
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(ResponseModel<IList<StudentDto>>.Ok(students));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var student= await _studentService.GetByIdAsync(id);
            return Ok(ResponseModel<StudentDto>.Ok(student));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var student = await _studentService.CreateAsync(dto);
          
            return StatusCode(201, ResponseModel<StudentDto>.Created(student));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
       int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _studentService.UpdateAsync(id, dto);
            return Ok(ResponseModel<StudentDto>.Ok(student));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }


    }
}
