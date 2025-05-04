using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models;

namespace HomeVital.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user.PatientID.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.Patient));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.PatientID.Value.ToString()));

                System.Console.WriteLine($"Role added: {Roles.Patient}");
            }

            if (user.HealthcareWorkerID.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.HealthcareWorker));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.HealthcareWorkerID.Value.ToString()));
                System.Console.WriteLine($"Role added: {Roles.HealthcareWorker}");
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
