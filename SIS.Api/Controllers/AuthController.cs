using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS.Application.Common;
using SIS.Application.DTOs;
using SIS.Application.Interfaces;
using static SIS.Application.DTOs.AuthDto;

namespace SIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(ResponseModel<AuthResponseDto>.Ok(result));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return StatusCode(201, ResponseModel<AuthResponseDto>.Created(result));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);
            return Ok(ResponseModel<string>.Ok(
            "Əgər bu email sistemdə mövcuddursa, şifrə sıfırlama linki göndərildi."));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);
            return Ok(ResponseModel<bool>.Ok(result));
        }
    }
}
