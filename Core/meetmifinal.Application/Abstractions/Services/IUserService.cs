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
        Task<User> AddUserAsync(User newUser);
        Task<User> UpdateUserAsync(User updatedUser);
        Task DeleteUserAsync(Guid id);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime tokenDate, int addTimeToTokenDate);
    }
}
