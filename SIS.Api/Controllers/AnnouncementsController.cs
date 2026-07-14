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
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _announcementService.GetAllAsync();
            return Ok(ResponseModel<IList<AnnouncementDto>>.Ok(announcements));
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFiltered([FromQuery] string? targetRole, [FromQuery] int? courseId)
        {
            var announcements = await _announcementService.GetFilteredAsync(targetRole, courseId);
            return Ok(ResponseModel<IList<AnnouncementDto>>.Ok(announcements));
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var announcements = await _announcementService.GetByCourseIdAsync(courseId);
            return Ok(ResponseModel<IList<AnnouncementDto>>.Ok(announcements));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementDto dto)
        {
            var announcement = await _announcementService.CreateAsync(dto);
            return StatusCode(201, ResponseModel<AnnouncementDto>.Created(announcement));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            await _announcementService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }
    }
}
