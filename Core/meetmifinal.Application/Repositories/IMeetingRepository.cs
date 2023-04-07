using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetByIdAsync(Guid id);
        Task<IEnumerable<Meeting>> GetAllAsync();
        Task AddAsync(Meeting meeting);
        Task UpdateAsync(Meeting meeting);
        Task DeleteAsync(Meeting meeting);
    }
}
