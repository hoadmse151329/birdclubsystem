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
        private readonly IMemberService _memberService;
        private readonly IMeetingService _meetingService;
        private readonly IMeetingParticipantService _meetParticipantService;
        private readonly IFieldTripService _fieldTripService;
        private readonly IFieldTripParticipantService _tripParticipantService;
        public StaffController(
            IMemberService memberService,
            IMeetingService meetingService,
            IMeetingParticipantService meetParticipantService,
            IFieldTripService fieldTripService,
            IFieldTripParticipantService tripParticipantService,
            IConfiguration config)
        {
            _memberService = memberService;
            _meetingService = meetingService;
            _meetParticipantService = meetParticipantService;
            _fieldTripService = fieldTripService;
            _tripParticipantService = tripParticipantService;
            _config = config;
        }

        /*[HttpPost("Profile")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Staff Details Not Found!"
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
        }*/

        [HttpPost("Profile")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Staff Details Not Found!"
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
                var result = await _meetParticipantService.UpdateAllMeetingParticipantStatus(listPart);
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
        [HttpPut("FieldTripParticipantStatus/Update/{id:int}")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(IEnumerable<FieldTripParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllFieldTripParticipantStatus(
            [Required][FromRoute] int id,
            [Required][FromBody] List<FieldTripParticipantViewModel> listPart)
        {
            try
            {
                var check = await _fieldTripService.GetBoolFieldTripId(id);
                if (!check)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field Trip does not exist!"
                    });
                }
                var result = await _tripParticipantService.UpdateAllFieldTripParticipantStatus(listPart);
                if (!result)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "All Field Trip Participant Status Update Failed!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result,
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
