using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using Microsoft.IdentityModel.Tokens;

namespace G_Transport.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(UserDto userDto)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"]));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userDto.Id.ToString()),
                new Claim(ClaimTypes.Email, userDto.Email),
            };
            foreach (var item in userDto.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
            }

            var token = new JwtSecurityToken
            (
                issuer: jwt["issuer"],
                audience: jwt["audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwt["expiryTime"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
