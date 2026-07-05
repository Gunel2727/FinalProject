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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _adminService.GetAllDepartmentsAsync();
            return Ok(ResponseModel<IList<DepartmentDto>>.Ok(departments));
        }

        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto dto)
        {
            var department = await _adminService.CreateDepartmentAsync(dto);
            return StatusCode(201, ResponseModel<DepartmentDto>.Created(department));
        }

        [HttpPut("departments/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto dto)
        {
            var department = await _adminService.UpdateDepartmentAsync(id, dto);
            return Ok(ResponseModel<DepartmentDto>.Ok(department));
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _adminService.DeleteDepartmentAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpGet("programmes")]
        public async Task<IActionResult> GetAllProgrammes()
        {
            var programmes = await _adminService.GetAllProgrammesAsync();
            return Ok(ResponseModel<IList<ProgrammeDto>>.Ok(programmes));
        }

        [HttpPost("programmes")]
        public async Task<IActionResult> CreateProgramme(
        [FromBody] CreateProgrammeDto dto)
        {
            var programme = await _adminService.CreateProgrammeAsync(dto);
            return StatusCode(201, ResponseModel<ProgrammeDto>.Created(programme));
        }

        [HttpPut("programmes/{id}")]
        public async Task<IActionResult> UpdateProgramme(
       int id, [FromBody] UpdateProgrammeDto dto)
        {
            var programme = await _adminService.UpdateProgrammeAsync(id, dto);
            return Ok(ResponseModel<ProgrammeDto>.Ok(programme));
        }

        [HttpGet("terms")]
        public async Task<IActionResult> GetAllTerms()
        {
            var terms = await _adminService.GetAllTermsAsync();
            return Ok(ResponseModel<IList<AcademicTermDto>>.Ok(terms));
        }

        [HttpPost("terms")]
        public async Task<IActionResult> CreateTerm([FromBody] CreateAcademicTermDto dto)
        {
            var term = await _adminService.CreateTermAsync(dto);
            return StatusCode(201, ResponseModel<AcademicTermDto>.Created(term));
        }

        [HttpPut("terms/{id}")]
        public async Task<IActionResult> UpdateTerm(int id, [FromBody] AcademicTermDto dto)
        {
            var term = await _adminService.UpdateTermAsync(id, dto);
            return Ok(ResponseModel<AcademicTermDto>.Ok(term));
        }
    }
}
