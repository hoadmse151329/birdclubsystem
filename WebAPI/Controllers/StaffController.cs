using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
using BAL.ViewModels.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

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
        private readonly IContestService _contestService;
        private readonly IContestParticipantService _contestParticipantService;
        public StaffController(
            IMemberService memberService,
            IMeetingService meetingService,
            IMeetingParticipantService meetParticipantService,
            IFieldTripService fieldTripService,
            IFieldTripParticipantService tripParticipantService,
            IContestService contestService,
            IContestParticipantService contestParticipantService,
            IConfiguration config)
        {
            _memberService = memberService;
            _meetingService = meetingService;
            _meetParticipantService = meetParticipantService;
            _fieldTripService = fieldTripService;
            _tripParticipantService = tripParticipantService;
            _contestService = contestService;
            _contestParticipantService = contestParticipantService;
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

        [HttpGet("Index")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(GetStaffDashboard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffDashboard()
        {
            try
            {
                int eventCount = await _meetingService.CountMeeting() + await _fieldTripService.CountFieldTrip() + await _contestService.CountContest();
                int eventOpen = await _meetingService.CountMeetingByStatus("OpenRegistration") + await _fieldTripService.CountFieldTripByStatus("OpenRegistration") + await _contestService.CountContestByStatus("OpenRegistration");
                int eventClosed = await _meetingService.CountMeetingByStatus("ClosedRegistration") + await _fieldTripService.CountFieldTripByStatus("ClosedRegistration") + await _contestService.CountContestByStatus("ClosedRegistration");
                int eventCheckingIn = await _meetingService.CountMeetingByStatus("CheckingIn") + await _fieldTripService.CountFieldTripByStatus("CheckingIn") + await _contestService.CountContestByStatus("CheckingIn");
                int eventOngoing = await _meetingService.CountMeetingByStatus("Ongoing") + await _fieldTripService.CountFieldTripByStatus("Ongoing") + await _contestService.CountContestByStatus("Ongoing");
                GetStaffDashboard result = new GetStaffDashboard()
                {
                    TotalEvents = eventCount,
                    TotalEventsOpenRegistration = eventOpen,
                    TotalEventsClosedRegistration = eventClosed,
                    TotalEventsCheckingIn = eventCheckingIn,
                    TotalEventsOngoing = eventOngoing
                };
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

        [HttpPut("MeetingStatus/Update/{id:int}")]
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
                if (!check) return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not exist!"
                    });
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
                    SuccessMessage = "Successfully update Check-In Status!",
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
        [HttpPut("FieldTripStatus/Update/{id:int}")]
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
                if (!check) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip does not exist!"
                });
                var result = await _tripParticipantService.UpdateAllFieldTripParticipantStatus(listPart);
                if (!result)return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "All Field Trip Participant Status Update Failed!"
                });
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Check-In Status!",
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
        [HttpPut("Contest/{id:int}/Participant/All/Status/Update")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(IEnumerable<ContestParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllContestParticipantStatus(
            [Required][FromRoute] int id,
            [Required][FromBody] List<ContestParticipantViewModel> listPart)
        {
            try
            {
                var check = await _contestService.GetById(id);
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest does not exist"
                });
                if (!check.Status.Equals("CheckingIn")) NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest status is not \"Checking\" In to use this feature"
                });
                var result = await _contestParticipantService.UpdateAllContestParticipantStatus(listPart);
                if (!result) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "All Contest Participant Status Update Failed"
                });
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Check-In Status!",
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
        [HttpPut("Contest/{id:int}/Participant/All/Score/Update")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(IEnumerable<ContestParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllContestParticipantScore(
            [Required][FromRoute] int id,
            [Required][FromBody] List<ContestParticipantViewModel> listPart)
        {
            try
            {
                var check = await _contestService.GetById(id);
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest does not exist"
                });
                if (!check.Status.Equals("Ongoing")) NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest status is not \"Ongoing\" to use this feature"
                });
                var result = await _contestParticipantService.UpdateAllContestParticipantScore(listPart);
                if (!result) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "All Contest Participant Score Update Failed"
                });
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Contest Scores!",
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
        [HttpPut("Profile/Update")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStaffDetails(
            [Required][FromBody] MemberViewModel member
            )
        {
            try
            {
                var result = await _memberService.GetById(member.MemberId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Staff Details Not Found!"
                    });
                }
                if (member.Status == null)
                {
                    member.Status = result.Status;
                }
                if (member.ImagePath == null)
                {
                    member.ImagePath = result.ImagePath;
                }
                if (member.UserId == 0)
                {
                    member.UserId = result.UserId;
                }
                _memberService.Update(member);
                result = await _memberService.GetById(member.MemberId);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Profile!",
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
