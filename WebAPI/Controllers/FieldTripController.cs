using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldTripController : ControllerBase
    {
        private readonly IFieldTripService _fieldTripService;
        private readonly IFieldTripParticipantService _participantService;
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public FieldTripController(
            IFieldTripService fieldTripService,
            IConfiguration config,
            IMemberService memberService,
            IUserService userService,
            IFieldTripParticipantService fieldTripParticipantService)
        {
            _fieldTripService = fieldTripService;
            _config = config;
            _memberService = memberService;
            _userService = userService;
            _participantService = fieldTripParticipantService;
        }

        [HttpGet("All")]
        [HttpGet]
        [ProducesResponseType(typeof(List<FieldTripViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFieldTrips()
        {
            try
            {
                var result = await _fieldTripService.GetAll();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of Field Trips Not Found!"
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
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFieldTripById([FromRoute] int id)
        {
            try
            {
                var result = await _fieldTripService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Field Trip Not Found!"
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
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create (
            [Required][FromBody] FieldTripViewModel trip)
        {
            try
            {
                trip.Status = "Preparing";
                _fieldTripService.Create(trip);
                return Ok(new
                {
                    Status = true,
                    Message = "Field Trip Create successfully!",
                    Data = trip
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
        [HttpPost("Register/{id}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(FieldTripParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var trip = await _fieldTripService.GetById(id);
                if (trip == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participantNo = await _participantService.Create(memId, id);
                return Ok(new
                {
                    Status = true,
                    Message = "Add Member Participation successfully !",
                    Data = participantNo
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
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFieldTripAndParticipantNo(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var trip = await _fieldTripService.GetById(id);
                if (trip == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.GetParticipationNo(memId, id);
                trip.ParticipationNo = participateNo;
                return Ok(new
                {
                    Status = true,
                    Message = "Get Field Trip successfully!",
                    Data = trip
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
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required][FromBody] FieldTripViewModel trip)
        {
            try
            {
                var result = _fieldTripService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field Trip does not exist!"
                    });
                }
                trip.TripId = id;
                _fieldTripService.Update(trip);
                result = await _fieldTripService.GetById(trip.TripId.Value);
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
        [HttpDelete("RemoveParticipant/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParticipant(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var trip = await _participantService.GetParticipationNo(memId, id);
                if (trip == 0) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var result = await _participantService.Delete(memId, id);
                return Ok(new
                {
                    Status = true,
                    Data = result,
                    SuccessMessage = "Remove Field Trip Participation successfully!"
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
