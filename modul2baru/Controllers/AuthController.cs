using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using modul2baru.Models;
using modul2baru.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace modul2baru.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private string __constr;
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                AuthContext authContext = new AuthContext(this.__constr);
                string roleName;
                Person user = authContext.Authenticate(loginDto.email, loginDto.password, out roleName);

                if (user != null)
                {
                    var token = GenerateJwtToken(user, roleName);
                    return Ok(new
                    {
                        status = 200,
                        message = "Login berhasil",
                        token = token
                    });
                }

                return Unauthorized(new { message = "Email atau Password salah" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal melakukan proses login", error = ex.Message });
            }
        }

        private string GenerateJwtToken(Person user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.nama),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim("id_person", user.id_person.ToString()),
                new Claim(ClaimTypes.Role, string.IsNullOrEmpty(role) ? "user" : role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}