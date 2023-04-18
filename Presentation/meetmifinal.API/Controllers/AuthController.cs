using MediatR;
using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.DTOs.UserLoginDto;
using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace meetmifinal.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [EnableCors]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AuthController(IUserService userService, IConfiguration configuration, IAuthService authService, IMediator mediator)
        {
            _userService = userService;
            _configuration = configuration;
            _authService = authService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<Token> Login([FromBody] UserLoginDto login)
        {
            if (login == null)
            {
                throw new Exception("Invalid login");
            }
            return await _authService.LoginAsync(login.Email, login.Password, 5);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> SignUp(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
