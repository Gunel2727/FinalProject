using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using SIS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Identity
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly GoogleAuthSettings _settings;

        public GoogleAuthService(IOptions<GoogleAuthSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<string?> ValidateTokenAndGetEmailAsync(string idToken)
        {
            try
            {
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _settings.ClientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
                return payload.Email;
            }
            catch (InvalidJwtException)
            {
                return null;
            }
        }
    }
}
