using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("conversation/{otherUserId}")]
        public async Task<IActionResult> GetConversation(int otherUserId)
        {
            var myUserId = int.Parse(User.FindFirst("userId")!.Value);
            var messages = await _messageService.GetConversationAsync(myUserId, otherUserId);
            return Ok(ResponseModel<IList<ChatMessageDto>>.Ok(messages));
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            var myUserId = int.Parse(User.FindFirst("userId")!.Value);
           var result= await _messageService.SendMessageAsync(myUserId, dto);
            return StatusCode(201, ResponseModel<ChatMessageDto>.Created(result));
        }
    }
}
