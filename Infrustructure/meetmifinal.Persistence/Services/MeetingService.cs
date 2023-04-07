using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using meetmifinal.Application.Abstractions.Services;
using meetmifinal.Application.Repositories;
using meetmifinal.Domain.Entities;

namespace meetmifinal.Persistence.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingsAsync()
        {
            return await _meetingRepository.GetAllAsync();
        }

        public async Task<Meeting> GetMeetingByIdAsync(Guid id)
        {
            return await _meetingRepository.GetByIdAsync(id);
        }

        public async Task<Meeting> AddMeetingAsync(Meeting newMeeting)
        {
            await _meetingRepository.AddAsync(newMeeting);
            return newMeeting;
        }

        public async Task<Meeting> UpdateMeetingAsync(Meeting updatedMeeting)
        {
            var meeting = await _meetingRepository.GetByIdAsync(updatedMeeting.Id);
            meeting.Name = updatedMeeting.Name;
            meeting.StartTime = updatedMeeting.StartTime;
            meeting.EndTime = updatedMeeting.EndTime;
            meeting.Description = updatedMeeting.Description;
            await _meetingRepository.UpdateAsync(meeting);
            return meeting;
        }

        public async Task DeleteMeetingAsync(Guid id)     
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            await _meetingRepository.DeleteAsync(meeting);
        }
    }
}
