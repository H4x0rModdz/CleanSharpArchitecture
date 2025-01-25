using CleanSharpArchitecture.Application.Interfaces.Services;
using CleanSharpArchitecture.Application.Services.Interfaces;
using CleanSharpArchitecture.Domain.Entities;
using CleanSharpArchitecture.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanSharpArchitecture.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

            if (string.IsNullOrWhiteSpace(_jwtSettings.SecretKey))
                throw new ArgumentNullException(nameof(_jwtSettings.SecretKey), "JWT Secret Key must be configured.");

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                ValidateIssuer = !string.IsNullOrWhiteSpace(_jwtSettings.Issuer),
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = !string.IsNullOrWhiteSpace(_jwtSettings.Audience),
                ValidAudience = _jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        }

        /// <summary>
        /// Gera um token JWT para o usuário fornecido.
        /// </summary>
        public Task<string> GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Task.FromResult(token);
        }

        /// <summary>
        /// Valida o token JWT.
        /// </summary>
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extrai o ID do usuário do token fornecido.
        /// </summary>
        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
