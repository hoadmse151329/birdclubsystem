using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
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
        private readonly IFieldTripDayByDayService _dayByDayService;
        private readonly IFieldTripInclusionService _inclusionService;
        private readonly IFieldTripAdditionalDetailService _addDetailService;
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public FieldTripController(
            IFieldTripService fieldTripService,
            IConfiguration config,
            IMemberService memberService,
            IUserService userService,
            IFieldTripParticipantService fieldTripParticipantService,
            IFieldTripDayByDayService dayByDayService,
            IFieldTripInclusionService inclusionService,
            IFieldTripAdditionalDetailService additionalDetailService
            )
        {
            _fieldTripService = fieldTripService;
            _config = config;
            _memberService = memberService;
            _userService = userService;
            _participantService = fieldTripParticipantService;
            _dayByDayService = dayByDayService;
            _inclusionService = inclusionService;
            _addDetailService = additionalDetailService;
        }

        [HttpPost("All")]
        [ProducesResponseType(typeof(List<FieldTripViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFieldTrips([Required][FromBody] string role)
        {
            try
            {
                var result = await _fieldTripService.GetAllFieldTrips(role);
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
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFieldTripById([FromRoute][Required] int id)
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
        [HttpGet("{id:int}/Lite")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFieldTripByIdLite([FromRoute][Required] int id)
        {
            try
            {
                var result = await _fieldTripService.GetByIdWithoutInclude(id);
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
        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create (
            [Required][FromBody] FieldTripViewModel trip)
        {
            try
            {
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
        [Authorize(Roles = "Manager")]
        [HttpGet("{id:int}/Cancel")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCancelFieldTrip(
            [Required][FromRoute] int id)
        {
            try
            {
                var result = await _fieldTripService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Fieldtrip does not exist!"
                    });
                }
                result.TripId = id;
                result.Status = "Cancelled";
                _fieldTripService.Update(result);
                result = await _fieldTripService.GetById(id);
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
        [HttpPost("{id:int}/Create/DayByDay")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldtripDaybyDayViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDayByDay(
            [Required][FromRoute] int id,
            [Required][FromBody] FieldtripDaybyDayViewModel tripDay)
        {
            try
            {
                if(await _dayByDayService.Create(id, tripDay))
                return Ok(new
                {
                    Status = true,
                    Message = "Field Trip Day Create successfully!",
                    BoolData = true
                });
                else return StatusCode(StatusCodes.Status500InternalServerError,new
                {
                    Status = true,
                    Message = "Field Trip Day Create Failed!",
                    BoolData = false
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
        [HttpPost("{id:int}/Create/Inclusion")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldtripInclusionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInclusion(
            [Required][FromRoute] int id,
            [Required][FromBody] FieldtripInclusionViewModel tripInclu)
        {
            try
            {
                if (await _inclusionService.Create(id, tripInclu))
                    return Ok(new
                    {
                        Status = true,
                        Message = "Field Trip Inclusion Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Field Trip Inclusion Create Failed!",
                    BoolData = false
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

        [HttpPost("{id:int}/Create/AdditionalDetail")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldtripInclusionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdditionalDetail(
            [Required][FromRoute] int id,
            [Required][FromBody] FieldTripAdditionalDetailViewModel tripAddDetail)
        {
            try
            {
                if (await _addDetailService.Create(id, tripAddDetail))
                    return Ok(new
                    {
                        Status = true,
                        Message = "Field Trip Additional Detail Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Field Trip Additional Detai Create Failed!",
                    BoolData = false
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
        [HttpPost("Register/{id:int}")]
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
                    IntData = participantNo
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
        [HttpPost("Participant/{id:int}")]
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
        [Authorize(Roles = "Manager,Staff")]
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
        [HttpPut("{id:int}/GettingThere/{getId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGettingThere(
            [Required][FromRoute] int id,
            [Required][FromRoute] int getId,
            [Required][FromBody] FieldtripGettingThereViewModel tripGet)
        {
            try
            {
                var result = _fieldTripService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip does not exist!"
                    });
                }
                tripGet.TripId = id;
                var check = _fieldTripService.UpdateGettingThere(tripGet);
                if (check)
                {
                    result = await _fieldTripService.GetById(tripGet.TripId.Value);
                    return Ok(new
                    {
                        Status = true,
                        Data = result
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field trip does not exist or internal server error"
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
        [HttpPut("{tripId:int}/DayByDay/{dayId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDayByDay(
            [Required][FromRoute] int tripId,
            [Required][FromRoute] int dayId,
            [Required][FromBody] FieldtripDaybyDayViewModel tripDay)
        {
            try
            {
                var check = _fieldTripService.GetById(tripId).Result;
                if (check == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip does not exist!"
                    });
                }
                var day = await _dayByDayService.GetById(dayId);
                if (day == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip Day By Day does not exist!"
                    });
                }
                var result = await _dayByDayService.Update(tripId, tripDay);
                if (result)
                {
                    return Ok(new
                    {
                        Status = true,
                        Data = result
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field trip does not exist or internal server error"
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
        [HttpPut("{tripId:int}/Inclusion/{incId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInclusion(
            [Required][FromRoute] int tripId,
            [Required][FromRoute] int incId,
            [Required][FromBody] FieldtripInclusionViewModel tripInc)
        {
            try
            {
                var result = _fieldTripService.GetById(tripId).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip does not exist!"
                    });
                }
                var inc = await _inclusionService.GetById(incId);
                if (inc == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip Inclusion does not exist!"
                    });
                }
                var check = await _inclusionService.Update(tripId, tripInc);
                if (check)
                {
                    return Ok(new
                    {
                        Status = true,
                        Data = check
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field trip does not exist or internal server error"
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
        [HttpPut("{tripId:int}/AdditionalDetail/{addDeId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdditionalDetail(
            [Required][FromRoute] int tripId,
            [Required][FromRoute] int addDeId,
            [Required][FromBody] FieldTripAdditionalDetailViewModel tripAddDe)
        {
            try
            {
                var result = _fieldTripService.GetById(tripId).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip does not exist!"
                    });
                }
                var inc = await _addDetailService.GetById(addDeId);
                if (inc == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Field trip Additional Detail does not exist!"
                    });
                }
                var check = await _addDetailService.Update(tripId, tripAddDe);
                if (check)
                {
                    return Ok(new
                    {
                        Status = true,
                        Data = check
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field trip does not exist or internal server error"
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
        [HttpPost("RemoveParticipant/{id}")]
        [Authorize(Roles = "Member,Manager")]
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
        [HttpGet("AllParticipants/{id}")]
        [ProducesResponseType(typeof(List<MeetingParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllParticipantByFieldTripId(
            [FromRoute] int id)
        {
            try
            {
                var result = await _participantService.GetAllByTripId(id);
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
        [HttpPost("Participation/AllFieldTrips")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<GetEventParticipation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFieldTripParticipations(
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
                        ErrorMessage = "List of Fieldtrip Participations Not Found!"
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
