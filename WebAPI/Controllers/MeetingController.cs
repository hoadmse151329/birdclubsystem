using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create (
            [Required] string meetingName,
            [Required] string description,
            [Required] DateTime registrationDeadline,
            [Required] DateTime startDate,
            [Required] DateTime endDate, 
            [Required] int numberOfParticipants, 
            [Required] string host, 
            [Required] string incharge, 
            [Required] string note)
        {
            try
            {
                MeetingViewModel value = new MeetingViewModel
                {
                    MeetingName = meetingName,
                    Description = description,
                    RegistrationDeadline = registrationDeadline,
                    StartDate = startDate,
                    EndDate = endDate,
                    NumberOfParticipants = numberOfParticipants,
                    Host = host,
                    Incharge = incharge,
                    Note = note
                };
                _meetingService.Create(value);
                return Ok(new
                {
                    Status = true,
                    Message = "Meeting Create successfully !",
                    Data = value
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
        [HttpPost("Register/{id}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(MeetingParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [Required] [FromRoute] int id,
            [Required] [FromBody] int usrId)
        {
            try
            {
                var meeting = await _meetingService.GetById(id);
                if(meeting == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var mem = await _userService.GetById(usrId);
                if(mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.Create(mem.MemberId, id);
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
            [Required][FromBody] int usrId)
        {
            try
            {
                var meeting = await _meetingService.GetById(id);
                if (meeting == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var mem = await _userService.GetById(usrId);
                if (mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.GetParticipationNo(mem.MemberId, id);
                meeting.ParticipationNo = participateNo;
                return Ok(new
                {
                    Status = true,
                    Message = "Get Meeting successfully !",
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
        [Authorize(Roles = "Manager")]
		[HttpPut("Update/{id}")]
		[ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required] string meetingname, 
            [Required] string description, 
            [Required] DateTime registrationDeadline, 
            [Required] DateTime startDate,
            [Required] DateTime endDate,
            [Required] int numberOfParticipants,
            [Required] string host,
            [Required] string incharge,
            [Required] string note)
        {
            try
            {
                var result = await _meetingService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not exist!"
                    });
                }
                result.MeetingName = meetingname;
                result.Description = description;
                result.RegistrationDeadline = registrationDeadline;
                result.StartDate = startDate;
                result.EndDate = endDate;
                result.NumberOfParticipants = numberOfParticipants;
                result.Host = host;
                result.Incharge = incharge;
                result.Note = note;
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
    }
}
