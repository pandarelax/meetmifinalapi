﻿using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Abstractions.Services
{
    public interface IMeetingService
    {
        Task<IEnumerable<Meeting>> GetAllMeetingsAsync();
        Task<Meeting> GetMeetingByIdAsync(Guid id);
        Task<Meeting> AddMeetingAsync(Meeting newMeeting);
        Task<Meeting> UpdateMeetingAsync(Meeting updatedMeeting);
        Task DeleteMeetingAsync(Guid id);
    }
}
