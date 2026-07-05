using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SIS.Application.Interfaces;
using SIS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Identity
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        
        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
             };

            var key = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(_jwtSettings.Secret));


            var credentials = new SigningCredentials(
           key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
           signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
