using CapstoneProject.Database.Model.Meta;
using CapstoneProject.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CapstoneProject.Infrastructure.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator()
        {
            ConfigurationBuilder configurationBuilder = new();

            _ = configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();
            _configuration = configuration;
        }
        public string GenerateToken(Guid id, UserRole role)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
