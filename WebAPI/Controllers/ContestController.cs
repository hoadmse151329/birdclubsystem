using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _contestService;
        private readonly IContestParticipantService _participantService;
        private readonly IConfiguration _config;
        public ContestController(
            IContestService contestService,
            IContestParticipantService contestParticipantService,
            IConfiguration config)
        {
            _contestService = contestService;
            _participantService = contestParticipantService;
            _config = config;
        }

        [HttpGet("All")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ContestViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllContests()
        {
            try
            {
                var result = await _contestService.GetAll();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of Contests Not Found!"
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
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetContestById([FromRoute] int id)
        {
            try
            {
                var result = await _contestService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Contest Not Found!"
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
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(
            string contestName,
            string description,
            DateTime registrationDeadline,
            DateTime startDate,
            DateTime endDate,
            int beforeScore,
            int afterScore,
            decimal fee,
            decimal prize,
            string host,
            string incharge,
            string note,
            string review,
            int numberOfParticipants,
            string status
            )
        {
            try
            {
                ContestViewModel value = new ContestViewModel
                {
                    ContestName = contestName,
                    Description = description,
                    RegistrationDeadline = registrationDeadline,
                    StartDate = startDate,
                    EndDate = endDate,
                    BeforeScore = beforeScore,
                    AfterScore = afterScore,
                    Status = status,
                    Fee = fee,
                    Prize = prize,
                    NumberOfParticipants = numberOfParticipants,
                    Host = host,
                    Incharge = incharge,
                    Note = note,
                    Review = review
                };
                _contestService.Create(value);
                return Ok(new
                {
                    Status = true,
                    Message = "Contest Create successfully!",
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
