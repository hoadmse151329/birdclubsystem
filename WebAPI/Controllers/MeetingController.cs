using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using BAL.ViewModels.Event;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IMeetingParticipantService _participantService;
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public MeetingController(
            IMeetingService meetingService, 
            IConfiguration config, 
            IMemberService memberService, 
            IUserService userService,
            IMeetingParticipantService meetingParticipantService)
        {
            _meetingService = meetingService;
            _config = config;
            _memberService = memberService;
            _userService = userService;
            _participantService = meetingParticipantService;
        }

		[HttpGet("All")]
		[HttpGet]
		[ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAllMeetings()
		{
			try
			{
				var result = await _meetingService.GetAll();
				if (result == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "List of Meetings Not Found!"
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
				// Log the exception if needed
				return BadRequest(new
				{
					Status = false,
					ErrorMessage = ex.Message
				});
			}
		}
        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingsByName(
            [FromQuery] int? meetingId,
            [FromQuery] string? meetingName,
            [FromQuery] DateTime? registrationDeadline,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? numberOfParticipants,
            [FromQuery] string? orderBy)
        {
            try
            {
                var result = _meetingService.GetSortedMeetings(
                    meetingId: meetingId,
                    meetingName: meetingName,
                    registrationDeadline: registrationDeadline,
                    startDate: startDate,
                    endDate: endDate,
                    numberOfParticipants: numberOfParticipants,
                    orderBy: orderBy);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of Meetings Not Found!"
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
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingById(
            [FromRoute] int id)
        {
            try
            {
                var result = await _meetingService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting Not Found!"
                    });
                }

                return Ok(new
                {
                    status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new
                {
                    status = false,
                    errorMessage = ex.Message
                });
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create (
            [Required][FromBody] MeetingViewModel meet)
        {
            try
            {
                meet.Status = "Preparing";
                _meetingService.Create(meet);

                return Ok(new
                {
                    Status = true,
                    Message = "Meeting Create successfully !",
                    Data = meet
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("Update/{id}")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required][FromBody] MeetingViewModel meet)
        {
            try
            {
                var result = _meetingService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not exist!"
                    });
                }
                meet.MeetingId = id;
                _meetingService.Update(meet);
                result = await _meetingService.GetById(meet.MeetingId.Value);
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("Update/Cancel/{id}")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCancelMeeting(
            [Required][FromRoute] int id)
        {
            try
            {
                var result = _meetingService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not exist!"
                    });
                }
                result.MeetingId = id;
                result.Status = "Cancelled";
                _meetingService.Update(result);
                result = await _meetingService.GetById(id);
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
        [HttpPost("Register/{id}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(MeetingParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [Required] [FromRoute] int id,
            [Required] [FromBody] string memId)
        {
            try
            {
                var meeting = await _meetingService.GetById(id);
                if(meeting == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if(!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.Create(memId, id);
                return Ok(new
                {
                    Status = true,
                    Message = "Add Member Participation successfully !",
                    Data = participateNo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
        [HttpPost("Participant/{id}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingAndParticipantNo(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var meeting = await _meetingService.GetById(id);
                if (meeting == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.GetParticipationNo(memId, id);
                meeting.ParticipationNo = participateNo;
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Get Meeting successfully !",
                    Data = meeting
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
        [HttpPost("RemoveParticipant/{id}")]
        [Authorize(Roles = "Member,Manager")]
        [ProducesResponseType(typeof(MeetingParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParticipant(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var meeting = await _participantService.GetParticipationNo(memId,id);
                if (meeting == 0) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var result = await _participantService.Delete(memId, id);
                return Ok(new
                {
                    Status = true,
                    Data = result,
                    SuccessMessage = "Remove Meeting Participation successfully !",
                });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new
                {
                    status = false,
                    errorMessage = ex.Message
                });
            }
        }
        [HttpGet("AllParticipants/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(List<MeetingParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllParticipantByMeetingId(
            [FromRoute] int id)
        {
            try
            {
                var result = await _participantService.GetAllByMeetingId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting Not Found!"
                    });
                }

                return Ok(new
                {
                    status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new
                {
                    status = false,
                    errorMessage = ex.Message
                });
            }
        }
        [HttpPost("Participation/AllMeetings")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<GetEventParticipation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMeetingParticipations(
            [Required][FromBody] string memId)
        {
            try
            {
                var result = await _participantService.GetAllByMemberIdInclude(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of Meeting Participations Not Found!"
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
