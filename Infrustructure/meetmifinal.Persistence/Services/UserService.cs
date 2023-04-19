using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.DTOs.User;
using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Application.Features.Commands.User.DeleteUser;
using meetmifinal.Application.Repositories;
using meetmifinal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<CreateUserCommandResponse> AddUserAsync(CreateUserDto model)
        {
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

        public async Task<User> UpdateUserAsync(Guid id, UserUpdateDto updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.PhotoUrl = updatedUser.PhotoUrl;
            await _userRepository.UpdateAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);

        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            return await _userRepository.CheckPasswordAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addTimeToTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addTimeToTokenDate);
                await _userRepository.UpdateAsync(user);
            }
            else
                throw new Exception("User not found");
        }
    }
}
