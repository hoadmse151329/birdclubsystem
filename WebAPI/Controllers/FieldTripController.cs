using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldTripController : ControllerBase
    {
        private readonly IFieldTripService _fieldTripService;
        private readonly IConfiguration _config;
        public FieldTripController(IFieldTripService fieldTripService, IConfiguration config)
        {
            _fieldTripService = fieldTripService;
            _config = config;
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
            string tripName,
            string description,
            string details,
            DateTime registrationDeadline,
            DateTime startDate,
            DateTime endDate,
            string status,
            decimal fee,
            int numberOfParticipants,
            string host,
            string incharge,
            string note,
            string review
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
    }
}
