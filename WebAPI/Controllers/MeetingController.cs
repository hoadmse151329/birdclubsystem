﻿using BAL.Services.Implements;
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
        private readonly IConfiguration _config;

        public MeetingController(IMeetingService meetingService, IConfiguration config, IMemberService memberService, IMeetingParticipantService meetingParticipantService)
        {
            _meetingService = meetingService;
            _config = config;
            _memberService = memberService;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingById([FromRoute] int id)
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
            string meetingName, 
            string description, 
            DateTime registrationDeadline, 
            DateTime startDate, 
            DateTime endDate, 
            int numberOfParticipants, 
            string host, 
            string incharge, 
            string note)
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
                var meeting = _meetingService.GetById(id);
                if(meeting == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var mem = await _memberService.GetById(usrId);
                if(mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.Create(usrId, id);
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
        [HttpPut("Update")]
        [Authorize(Roles = "Manager")]
		[HttpPut("Update/{id}")]
		[ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] MeetingViewModel meeting)
        {
            try
            {
                var result = await _meetingService.GetById(meeting.MeetingId.Value);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting does not exist!"
                    });
                }
                _meetingService.Update(meeting);
                result = await _meetingService.GetById(meeting.MeetingId.Value);
                return Ok(new
                {
                    status = true,
                    result
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
	}
}
