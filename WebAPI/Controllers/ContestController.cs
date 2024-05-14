using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IMemberService _memberService;
        private readonly IBirdService _birdService;
        public ContestController(
            IContestService contestService,
            IContestParticipantService contestParticipantService,
            IMemberService memberService,
            IBirdService birdService,
            IConfiguration config)
        {
            _contestService = contestService;
            _memberService = memberService;
            _participantService = contestParticipantService;
            _birdService = birdService;
            _config = config;
        }

        [HttpPost("All")]
        [ProducesResponseType(typeof(List<ContestViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllContests([FromBody] string? role)
        {
            try
            {
                var result = await _contestService.GetAllContests(role);
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
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [Required][FromBody] ContestViewModel contest)
        {
            try
            {
                _contestService.Create(contest);
                return Ok(new
                {
                    Status = true,
                    Message = "Contest Create successfully!",
                    Data = contest
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

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Manager,Staff")]
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required][FromBody] ContestViewModel contest)
        {
            try
            {
                var result = await _contestService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Contest does not exist!"
                    });
                }
                contest.ContestId = id;
                _contestService.Update(contest);
                result = await _contestService.GetById(contest.ContestId.Value);
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

        [HttpGet("Update/Cancel/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCancelContest([Required][FromRoute] int id)
        {
            try
            {
                var result = _contestService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Contest does not exist"
                    });
                }
                result.ContestId = id;
                result.Status = "Cancelled";
                _contestService.Update(result);
                result = await _contestService.GetById(id);
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

        [HttpPost("{contestId:int}/Bird/{birdId:int}/Register")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(ContestParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [Required][FromRoute] int contestId,
            [Required][FromRoute] int birdId,
            [Required][FromBody] string memberId)
        {
            try
            {
                var contest = await _contestService.GetById(contestId);
                if (contest == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest Not Found!"
                });
                var mem = await _memberService.GetBoolById(memberId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                var bird = await _birdService.GetById(birdId);
                if (bird == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Bird Not Found!"
                });
                int participateNo = await _participantService.Create(contestId, memberId, birdId);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Add Member Participation successfully!",
                    Data = participateNo
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

        [HttpPost("{id:int}/Participant")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetContestAndParticipantNoAndMemberBirds(
            [Required][FromRoute] int id,
            [Required][FromBody] string memId)
        {
            try
            {
                var cont = await _contestService.GetById(id);
                if (cont == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                var membirds = await _birdService.GetBirdsByMemberId(memId);
                int participateNo = await _participantService.GetParticipationNo(id, memId);
                cont.ParticipationNo = participateNo;
                cont.MemberBirdSelection = membirds.ToList();
                return Ok(new
                {
                    Status = true,
                    Message = "Get Contest successfully!",
                    Data = cont
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

        [HttpPost("{contestId:int}/Participant/Remove")]
        [Authorize(Roles = "Member,Manager")]
        [ProducesResponseType(typeof(ContestParticipantViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveParticipant(
            [Required][FromRoute] int contestId,
            [Required][FromBody] string memId)
        {
            try
            {
                var contest = await _participantService.GetParticipationNo(contestId, memId);
                if (contest == 0) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest Not Found!"
                });
                var mem = await _memberService.GetBoolById(memId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                var result = await _participantService.Delete(contestId, memId);
                return Ok(new
                {
                    Status = true,
                    Data = result,
                    SuccessMessage = "Remove Contest Participation Successfully!"
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
        [Authorize(Roles = "Manager, Staff")]
        [ProducesResponseType(typeof(List<ContestParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllParticipantsByContestId([FromRoute][Required] int id)
        {
            try
            {
                var result = await _participantService.GetAllByContestId(id);
                if (result == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest Not Found!"
                });
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
        [HttpPost("Participation/AllContests")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<GetEventParticipation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllContestParticipations([Required][FromBody] string memId)
        {
            try
            {
                var result = await _participantService.GetAllByMemberIdInclude(memId);
                if (result == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "List of Contest Participations Not Found!"
                });
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
