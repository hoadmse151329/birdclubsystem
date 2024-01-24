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
                var result = await _fieldTripService.GetAllFieldTrips();
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
                var result = await _fieldTripService.GetFieldTripById(id);
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
            [Required] string tripName,
            [Required] string description,
            [Required] string details,
            [Required] DateTime registrationDeadline,
            [Required] DateTime startDate,
            [Required] DateTime endDate,
            [Required] bool status,
            [Required] decimal fee,
            [Required] int numberOfParticipants,
            [Required] string host,
            [Required] string incharge,
            [Required] string note,
            [Required] string review
            )
        {
            try
            {
                FieldTripViewModel value = new FieldTripViewModel
                {
                    TripName = tripName,
                    Description = description,
                    Details = details,
                    RegistrationDeadline = registrationDeadline,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = status,
                    Fee = fee,
                    NumberOfParticipants = numberOfParticipants,
                    Host = host,
                    InCharge = incharge,
                    Note = note,
                    Review = review
                };
                _fieldTripService.Create(value);
                return Ok(new
                {
                    Status = true,
                    Message = "Field Trip Create successfully!",
                    Data = value
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
            [Required][FromBody] int usrId)
        {
            try
            {
                var trip = await _fieldTripService.GetFieldTripById(id);
                if (trip == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var mem = await _userService.GetById(usrId);
                if (mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participantNo = await _participantService.Create(mem.MemberId, id);
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
            [Required][FromBody] int usrId)
        {
            try
            {
                var trip = await _fieldTripService.GetFieldTripById(id);
                if (trip == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var mem = await _userService.GetById(usrId);
                if (mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                int participateNo = await _participantService.GetParticipationNo(mem.MemberId, id);
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
            [Required] string tripName,
            [Required] string description,
            [Required] string details,
            [Required] DateTime registrationDeadline,
            [Required] DateTime startDate,
            [Required] DateTime endDate,
            [Required] bool status,
            [Required] decimal fee,
            [Required] int numberOfParticipants,
            [Required] string host,
            [Required] string incharge,
            [Required] string note,
            [Required] string review)
        {
            try
            {
                var result = await _fieldTripService.GetFieldTripById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field Trip does not exist!"
                    });
                }
                result.TripName = tripName;
                result.Description = description;
                result.Details = details;
                result.NumberOfParticipants = numberOfParticipants;
                result.RegistrationDeadline = registrationDeadline;
                result.StartDate = startDate;
                result.EndDate = endDate;
                result.Status = status;
                result.Fee = fee;
                result.NumberOfParticipants = numberOfParticipants;
                result.Host = host;
                result.InCharge = incharge;
                result.Note = note;
                result.Review = review;
                _fieldTripService.Update(result);
                result = await _fieldTripService.GetFieldTripById(id);
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
        public async Task<IActionResult> RemoveParticipant(int id, string memId)
        {
            try
            {
                var trip = _fieldTripService.GetFieldTripById(id);
                if (trip == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip Not Found!"
                });
                var mem = await _memberService.GetById(memId);
                if (mem == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                var result = await _participantService.Delete(memId, id);
                return Ok(result);
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
