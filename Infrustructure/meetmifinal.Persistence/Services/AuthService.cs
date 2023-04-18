using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.DTOs.User;
using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Application.Repositories;
using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IUserService _userService;
        readonly ITokenService _tokenService;
        readonly IUserRepository _userRepository;

        public AuthService(IUserService userService, ITokenService tokenService, IUserRepository userRepository)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<Token> LoginAsync(string email, string password, int tokenLifeTime)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            string hashedPassword = HashPassword(password);

            if (user == null)
            {
                throw new Exception("This email is not exist");
            }

            var result = await _userService.CheckPasswordAsync(email, hashedPassword);
            if (result == null)
            {
                throw new Exception();
            }
            Token token = _tokenService.CreateAccessToken(user, tokenLifeTime);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;


        }

        // SignUp method for user
        public async Task<CreateUserCommandResponse> SignUpAsync(CreateUserDto model)
        {

            if (await CheckEmailExist(model.Email))
            {
                throw new Exception("This email is already exist");
            }

            model.Password = HashPassword(model.Password);

            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
            };

            await _userRepository.AddAsync(user);

            CreateUserCommandResponse response = new();

            return response;
        }

        //Hash password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //check if email exist is db
        private async Task<bool> CheckEmailExist(string email)
        {
            return await _userRepository.CheckEmailExistAsync(email);
        }
    }
}
