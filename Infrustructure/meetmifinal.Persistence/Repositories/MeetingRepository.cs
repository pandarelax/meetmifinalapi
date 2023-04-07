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
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MeetmiDbContext _dbContext;

        public MeetingRepository(MeetmiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Meeting> GetByIdAsync(Guid id)
        {
            return await _dbContext.Meetings.FindAsync(id);
        }

        public async Task<IEnumerable<Meeting>> GetAllAsync()
        {
            return await _dbContext.Meetings.ToListAsync();
        }

        public async Task AddAsync(Meeting meeting)
        {
            await _dbContext.Meetings.AddAsync(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            _dbContext.Meetings.Update(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Meeting meeting)
        {
            _dbContext.Meetings.Remove(meeting);
            await _dbContext.SaveChangesAsync();
        }
    }
}
