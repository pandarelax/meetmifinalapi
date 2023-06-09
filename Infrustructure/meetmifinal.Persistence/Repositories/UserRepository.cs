﻿using meetmifinal.Application.Features.Commands.User.CreateUser;
using meetmifinal.Application.Features.Commands.User.DeleteUser;
using meetmifinal.Application.Repositories;
using meetmifinal.Domain.Entities;
using meetmifinal.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeetmiDbContext _dbContext;

        public UserRepository(MeetmiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == user.ToString() && u.Password == password);
        }

        //check if email exists in database
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
