using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using static SIS.Application.DTOs.AiChatDto;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class AiController : ControllerBase
    {
        private readonly IAiChatService _aiChatService;

        public AiController(IAiChatService aiChatService)
        {
            _aiChatService = aiChatService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] AiChatRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest(ResponseModel<object>.Fail(400, "Mesaj boş ola bilməz."));

            var reply = await _aiChatService.AskAsync(dto.Message);
            return Ok(ResponseModel<AiChatResponseDto>.Ok(new AiChatResponseDto { Reply = reply }));
        }
    }
}
