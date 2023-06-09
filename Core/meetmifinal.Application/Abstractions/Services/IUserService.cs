﻿using meetmifinal.Application.DTOs.User;
using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Application.Features.Commands.User.DeleteUser;
using meetmifinal.Application.Features.Queries.User.GetByIdUser;
using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<CreateUserCommandResponse> AddUserAsync(CreateUserDto model);
        Task<User> UpdateUserAsync(Guid id, UserUpdateDto updatedUser);
        Task DeleteUserAsync(Guid id);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime tokenDate, int addTimeToTokenDate);
    }
}
