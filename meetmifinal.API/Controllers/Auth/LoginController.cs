using meetmifinal.models.Entities;
using meetmifinal.services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace meetmifinal.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] User login)
        {
            
            var user = await _userService.GetUserByEmailAsync(login.Email);
            if (user != null && await _userService.CheckPasswordAsync(user, login.Password))
            {
                var token = GenerateJwtTokenForUserAsync(user.Email, user.Id.ToString());
                return Ok(new { token });
            }
            return Unauthorized();
        }

        public async Task<string> GenerateJwtTokenForUserAsync(string email, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.NameIdentifier, userId)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return  tokenHandler.WriteToken(token);
        }


    }
}
