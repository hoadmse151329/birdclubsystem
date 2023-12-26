using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IConfiguration _config;

        public MeetingController(IMeetingService meetingService, IConfiguration config)
        {
            _meetingService = meetingService;
            _config = config;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMeetingById(int id)
        {
            try
            {
                var result = _meetingService.GetById(id);
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

        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create (string meetingName, string description, DateTime registrationDeadline, DateTime startDate, DateTime endDate, int numberOfParticipants, string host, string incharge, string note)
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
                    status = true,
                    Message = "Meeting Create successfully !",
                    value
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    errorMessage = ex.Message
                });
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MeetingViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, string meetingname, string description, DateTime registrationDeadline, DateTime startDate, DateTime endDate, int numberOfParticipants, string host, string incharge, string note)
        {
            try
            {
                var result = _meetingService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Meeting does not exist!"
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
                result = _meetingService.GetById(id);
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
