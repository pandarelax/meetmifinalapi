using meetmifinal.api.Services;
using meetmifinal.models.Entities;
using meetmifinal.services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetAll()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> GetById([FromRoute] Guid id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> Create([FromBody] Meeting meeting)
        {
            meeting.Id = Guid.NewGuid();
            await _meetingService.AddMeetingAsync(meeting);
            return CreatedAtAction(nameof(GetById), new { id = meeting.Id }, meeting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return BadRequest();
            }
            await _meetingService.UpdateMeetingAsync(meeting);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _meetingService.DeleteMeetingAsync(id);
            return NoContent();
        }
    }
}
