using MediatR;
using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.DTOs.User;
using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Application.Features.Commands.User.DeleteUser;
using meetmifinal.Application.Features.Queries.User.GetAllUser;
using meetmifinal.Application.Features.Queries.User.GetByIdUser;
using meetmifinal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public UserController(IUserService userService, IAuthService authService, IMediator mediator)
        {
            _userService = userService;
            _authService = authService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            GetAllUserQueryResponse response = await _mediator.Send(new GetAllUserQueryRequest());
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetById([FromRoute] GetByIdUserRequest request)
        {
            GetByIdUserResponse response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto newUser)
        {

            await _userService.UpdateUserAsync(id, newUser);

            return Ok("User created");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommandRequest request)
        {
            DeleteUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("yourSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
