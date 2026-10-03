using backend_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_API.Security
{
    public class JwtHandler
    {
        private readonly IConfiguration _config;

        public JwtHandler(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(ClaimsModel claimsModel)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, claimsModel.PublicId),
                new Claim(ClaimTypes.Email, claimsModel.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"], 
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                signingCredentials: creds
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
