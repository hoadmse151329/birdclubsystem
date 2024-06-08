using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
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
        private readonly IFieldTripMediaService _mediaService;
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
            IFieldTripAdditionalDetailService additionalDetailService,
            IFieldTripMediaService mediaService
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
            _mediaService = mediaService;
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

        [HttpPost("Search")]
        [ProducesResponseType(typeof(List<FieldTripViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFieldTripsByAttributes(
            [FromBody] string? role,
            [FromQuery] int? tripId,
            [FromQuery] string? tripName,
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
                var result = await _fieldTripService.GetSortedFieldTrips(
                    tripId: tripId,
                    tripName: tripName,
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
                        ErrorMessage = "List of FieldTrips Not Found!"
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
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(List<FieldTripViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffAllFieldTrips(
            [Required][FromBody] string? accToken
            )
        {
            try
            {
                var result = await _fieldTripService.GetAllFieldTrips("Staff",accToken);
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

        [HttpPost("Staff/Search")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(List<FieldTripViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffFieldTripsByAttributes(
            [Required][FromBody] string? accToken,
            [FromQuery] int? tripId,
            [FromQuery] string? tripName,
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
                var result = await _fieldTripService.GetSortedFieldTrips(
                    tripId: tripId,
                    tripName: tripName,
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
                        ErrorMessage = "List of FieldTrips Not Found!"
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
                        Status = false,
                        ErrorMessage = "Field Trip Not Found!"
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
        [HttpPost("{id}")]
        [Authorize(Roles = "Staff")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffFieldtripById(
            [FromRoute] int id,
            [FromBody][Required] string accToken
            )
        {
            try
            {
                var result = await _fieldTripService.GetByIdCheckIncharge(id, accToken);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Fieldtrip Not Found!"
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
                        Status = false,
                        ErrorMessage = "Field Trip Not Found!"
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
        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create (
            [Required][FromBody] CreateNewFieldtripVM trip)
        {
            try
            {
                _fieldTripService.Create(trip);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Field Trip Create successfully!",
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
                    SuccessMessage = "Field Trip Day Create successfully!",
                    BoolData = true
                });
                else return StatusCode(StatusCodes.Status500InternalServerError,new
                {
                    Status = true,
                    ErrorMessage = "Field Trip Day Create Failed!",
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
                        SuccessMessage = "Field Trip Inclusion Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    ErrorMessage = "Field Trip Inclusion Create Failed!",
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
                        SuccessMessage = "Field Trip Additional Detail Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    ErrorMessage = "Field Trip Additional Detai Create Failed!",
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
                    SuccessMessage = "Successfully registered in a field trip!",
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
                    SuccessMessage = "Get Field Trip successfully!",
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
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required][FromBody] UpdateFieldtripDetailsVM trip)
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
                    SuccessMessage = "Successfully updated Field Trip!",
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
        [HttpPut("{id:int}/Status/Update")]
        [Authorize(Roles = "Manager,Staff")]
        [ProducesResponseType(typeof(FieldTripViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [Required][FromRoute] int id,
            [Required][FromBody] UpdateFieldtripStatusVM trip)
        {
            try
            {
                var result = _fieldTripService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Fieldtrip does not exist!"
                    });
                }
                trip.TripId = id;
                var isUpdated = await _fieldTripService.UpdateStatus(trip);
                if (!isUpdated)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Fieldtrip does not have enough participants to close registration!"
                    });
                }
                result = await _fieldTripService.GetById(trip.TripId.Value);
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
                        SuccessMessage = "Successfully updated Field Trip Getting There details!",
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
                        SuccessMessage = "Successfully updated Field Trip Day By Day details!",
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
                        ErrorMessage = "Field Trip Inclusion does not exist!"
                    });
                }
                var check = await _inclusionService.Update(tripId, tripInc);
                if (check)
                {
                    return Ok(new
                    {
                        Status = true,
                        SuccessMessage = "Successfully updated Field Trip Inclusion details",
                        Data = check
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Field Trip does not exist or internal server error"
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
                        SuccessMessage = "Successfully updated Field Trip Additional details!",
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
        [HttpPost("{id:int}/Create/Media")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(FieldtripMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMeetingMedia(
            [Required][FromRoute] int id,
            [Required][FromBody] FieldtripMediaViewModel media)
        {
            try
            {
                if (await _mediaService.Create(id, media))
                    return Ok(new
                    {
                        Status = true,
                        SuccessMessage = "Fieldtrip Media Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Fieldtrip Media Create Failed!",
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
        [HttpPut("{tripId:int}/Media/{pictureId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMeetingMedia(
            [Required][FromRoute] int tripId,
            [Required][FromRoute] int pictureId,
            [Required][FromBody] FieldtripMediaViewModel media)
        {
            try
            {
                var check = _fieldTripService.GetById(tripId).Result;
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Fieldtrip does not exist!"
                });
                var pic = await _mediaService.GetById(pictureId);
                if (pic == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Fieldtrip Media does not exist!"
                });
                var result = await _mediaService.Update(tripId, media);
                if (result) return Ok(new
                {
                    Status = true,
                    BoolData = result
                });
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Fieldtrip does not exist or internal server error"
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
                    SuccessMessage = "Successfully deregistered!"
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
