using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.Repositories;
using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (user == null)
            {
                throw new Exception("This email is not exist");
            }

            var result = await _userService.CheckPasswordAsync(email, password);
            if (result == null)
            {
                throw new Exception();
            }
            Token token = _tokenService.CreateAccessToken(user, tokenLifeTime);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;


        }

        // SignUp method for user
        public async Task<User> SignUpAsync(User newUser)
        {
            await _userRepository.AddAsync(newUser);
            return newUser;
        }
    }
}
