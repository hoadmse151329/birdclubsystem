using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMeetingService _meetingService;
        private readonly IMeetingParticipantService _participantService;
        public StaffController(IMeetingService meetingService, IMeetingParticipantService participantService,  IConfiguration config)
        {
            _meetingService = meetingService;
            _participantService = participantService;
            _config = config;
        }

        [HttpPost("Profile")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffDetailsByUsrId([FromBody] string usrId)
        {
            return null;
        }

        [HttpPut("MeetingParticipantStatus/Update/{id:int}")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(IEnumerable<MeetingParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllMeetingParticipantStatus(
            [Required][FromRoute] int id,
            [Required][FromBody] List<MeetingParticipantViewModel> listPart)
        {
            try
            {
                var check = await _meetingService.GetBoolMeetingId(id);
                if (!check)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not exist!"
                    });
                }
                var result = await _participantService.UpdateAllMeetingParticipantStatus(listPart);
                if (!result)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "All Meeting Participant Status Update Failed!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException.Message
                    });
                }
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
