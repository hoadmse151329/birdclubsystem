﻿using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
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
        private readonly IMeetingMediaService _mediaService;
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public MeetingController(
            IMeetingService meetingService,
            IConfiguration config,
            IMemberService memberService,
            IUserService userService,
            IMeetingParticipantService meetingParticipantService,
            IMeetingMediaService mediaService)
        {
            _meetingService = meetingService;
            _config = config;
            _memberService = memberService;
            _userService = userService;
            _participantService = meetingParticipantService;
            _mediaService = mediaService;
        }

        [HttpPost("All")]
        [ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMeetings(
            [FromBody] string? role
            )
        {
            try
            {
                var result = await _meetingService.GetAllMeetings(role);
                if (result == null || !result.Any())
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("Staff/All")]
        [Authorize(Roles ="Staff")]
        [ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffAllMeetings(
            [Required][FromBody] string? accToken
            )
        {
            try
            {
                var result = await _meetingService.GetAllMeetings("Staff", accToken);
                if (result == null || !result.Any())
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("Staff/Search")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffMeetingsByAttributes(
            [Required][FromBody] string? accToken,
            [FromQuery] int? meetingId,
            [FromQuery] string? meetingName,
            [FromQuery] DateTime? openRegistration,
            [FromQuery] DateTime? registrationDeadline,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? numberOfParticipants,
            [FromQuery] List<string>? road,
            [FromQuery] List<string>? district,
            [FromQuery] List<string>? city,
            [FromQuery] List<string>? status,
            [FromQuery] string? orderBy)
        {
            try
            {
                bool isMemberOrGuest = false;
                var result = await _meetingService.GetSortedMeetings(
                    meetingId: meetingId,
                    meetingName: meetingName,
                    openRegistration: openRegistration,
                    registrationDeadline: registrationDeadline,
                    startDate: startDate,
                    endDate: endDate,
                    numberOfParticipants: numberOfParticipants,
                    roads: road,
                    districts: district,
                    cities: city,
                    statuses: status,
                    orderBy: orderBy,
                    isMemberOrGuest,
                    accToken: accToken
                    );
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("Search")]
        [ProducesResponseType(typeof(List<MeetingViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingsByAttributes(
            [FromBody] string? role,
            [FromQuery] int? meetingId,
            [FromQuery] string? meetingName,
            [FromQuery] DateTime? openRegistration,
            [FromQuery] DateTime? registrationDeadline,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? numberOfParticipants,
            [FromQuery] List<string>? road,
            [FromQuery] List<string>? district,
            [FromQuery] List<string>? city,
            [FromQuery] List<string>? status,
            [FromQuery] string? orderBy)
        {
            try
            {
                bool isMemberOrGuest = false;
                if (string.IsNullOrEmpty(role) || string.IsNullOrWhiteSpace(role) || role.Equals("Member") || role.Equals("Guest"))
                {
                    isMemberOrGuest = true;
                }
                var result = await _meetingService.GetSortedMeetings(
                    meetingId: meetingId,
                    meetingName: meetingName,
                    openRegistration: openRegistration,
                    registrationDeadline: registrationDeadline,
                    startDate: startDate,
                    endDate: endDate,
                    numberOfParticipants: numberOfParticipants,
                    roads: road,
                    districts: district,
                    cities: city,
                    statuses: status,
                    orderBy: orderBy,
                    isMemberOrGuest);
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("{id}")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffMeetingById(
            [FromRoute] int id,
            [FromBody][Required] string accToken)
        {
            try
            {
                var result = await _meetingService.GetByIdCheckIncharge(id,accToken);
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [Required][FromBody] CreateNewMeetingVM meet)
        {
            try
            {
                _meetingService.Create(meet);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Meeting Create successfully !",
                    Data = meet
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
        [HttpPut("{id:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDetails(
            [Required][FromRoute] int id,
            [FromBody] UpdateMeetingDetailsVM meet)
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPut("{id:int}/Status/Update")]
        [Authorize(Roles = "Manager,Staff")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [Required][FromRoute] int id,
            [FromBody] UpdateMeetingStatusVM meet)
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
                var isUpdated = await _meetingService.UpdateStatus(meet);
                if (!isUpdated)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Meeting does not have enough participants to close registration!"
                    });
                }
                result = await _meetingService.GetById(meet.MeetingId.Value);
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("{id:int}/Cancel")]
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
                    SuccessMessage = "Successfully Cancelled Meeting!",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("{id:int}/Create/Media")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMeetingMedia(
            [Required][FromRoute] int id,
            [Required][FromBody] MeetingMediaViewModel media)
        {
            try
            {
                if (await _mediaService.Create(id, media))
                    return Ok(new
                    {
                        Status = true,
                        SuccessMessage = "Meeting Media Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Meeting Media Create Failed!",
                    BoolData = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPut("{meetingId:int}/Media/{pictureId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMeetingMedia(
            [Required][FromRoute] int meetingId,
            [Required][FromRoute] int pictureId,
            [Required][FromBody] MeetingMediaViewModel media)
        {
            try
            {
                var check = _meetingService.GetById(meetingId).Result;
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting does not exist!"
                });
                var pic = await _mediaService.GetById(pictureId);
                if (pic == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Media does not exist!"
                });
                var result = await _mediaService.Update(meetingId, media);
                if (result) return Ok(new
                {
                    Status = true,
                    BoolData = result
                });
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting does not exist or internal server error"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("Register/{id}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(MeetingParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
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
                int participateNo = await _participantService.Create(memId, id);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully registered in a meeting!",
                    IntData = participateNo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
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
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("{id:int}/Participant/Remove")]
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
                var meeting = await _participantService.GetParticipationNo(memId, id);
                if (meeting == 0) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Meeting Not Found!"
                });
                var result = await _participantService.Delete(memId, id);
                return Ok(new
                {
                    Status = true,
                    BoolData = result,
                    SuccessMessage = "Successfully deregistered!",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpGet("AllParticipants/{id}")]
        [Authorize(Roles = "Manager, Staff")]
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
                        Status = false,
                        ErrorMessage = "Meeting Not Found!"
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpGet("Participant/{id}/{memberId}")]
        [Authorize(Roles = "Manager, Staff")]
        [ProducesResponseType(typeof(List<MeetingParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetParticipant(
            [Required][FromRoute] int id,
            [Required][FromRoute] string memberId)
        {
            try
            {
                var result = await _participantService.GetById(memberId, id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting Participant Not Found!"
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        /*[HttpPut("UpdateParticipants/{id}")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(List<MeetingParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCheckInStatus(
            [Required][FromRoute] int id,
            [Required][FromQuery] string memberId,
            [Required][FromBody] MeetingParticipantViewModel meet)
        {
            try
            {
                var result = _participantService.GetById(memberId, id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting Participant Not Found!"
                    });
                }

                result.CheckInStatus = meet.CheckInStatus;
                _participantService.Update(result);
                result = await _participantService.GetById(memberId, id);
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
    }
}
