using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
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
        private readonly IContestMediaService _mediaService;
        private readonly IConfiguration _config;
        private readonly IMemberService _memberService;
        private readonly IBirdService _birdService;
        public ContestController(
            IContestService contestService,
            IContestParticipantService contestParticipantService,
            IContestMediaService mediaService,
            IMemberService memberService,
            IBirdService birdService,
            IConfiguration config
            )
        {
            _contestService = contestService;
            _memberService = memberService;
            _participantService = contestParticipantService;
            _birdService = birdService;
            _config = config;
            _mediaService = mediaService;
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

        [HttpPost("Search")]
        [ProducesResponseType(typeof(List<ContestViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetContestsByAttributes(
            [FromBody] string? role,
            [FromQuery] int? tripId,
            [FromQuery] string? tripName,
            [FromQuery] DateTime? openRegistration,
            [FromQuery] DateTime? registrationDeadline,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? numberOfParticipants,
            [FromQuery] int? reqMinElo,
            [FromQuery] int? reqMaxElo,
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
                var result = await _contestService.GetSortedContests(
                    tripId: tripId,
                    tripName: tripName,
                    openRegistration: openRegistration,
                    registrationDeadline: registrationDeadline,
                    startDate: startDate,
                    endDate: endDate,
                    numberOfParticipants: numberOfParticipants,
                    reqMinElo: reqMinElo,
                    reqMaxElo: reqMaxElo,
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
                        Status = false,
                        ErrorMessage = "Contest Not Found!"
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
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [Required][FromBody] CreateNewContestVM contest)
        {
            try
            {
                _contestService.Create(contest);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Contest Create successfully!",
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

        /*[HttpPut("Update/{id}")]
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
                    SuccessMessage = "Successfully updated Contest!",
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

        [HttpPut("{id:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [Required][FromBody] UpdateContestDetailsVM contest)
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
                    SuccessMessage = "Successfully updated Contest!",
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
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [Required][FromRoute] int id,
            [FromBody] UpdateContestStatusVM meet)
        {
            try
            {
                var result = _contestService.GetById(id).Result;
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Contest does not exist!"
                    });
                }
                meet.ContestId = id;
                var isUpdated = await _contestService.UpdateStatus(meet);
                if (!isUpdated)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Contest does not have enough participants to close registration!"
                    });
                }
                result = await _contestService.GetById(meet.ContestId.Value);
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
        [HttpGet("{id:int}/Cancel")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCancelContest(
            [Required][FromRoute] int id
            )
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
                    SuccessMessage = "Successfully cancelled Contest",
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
                    SuccessMessage = "Successfully registered in a contest!",
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
                    SuccessMessage = "Get Contest successfully!",
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
                    SuccessMessage = "Successfully deregistered!",
                    Data = result,
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
        [HttpPost("{id:int}/Create/Media")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMeetingMedia(
            [Required][FromRoute] int id,
            [Required][FromBody] ContestMediaViewModel media)
        {
            try
            {
                if (await _mediaService.Create(id, media))
                    return Ok(new
                    {
                        Status = true,
                        SuccessMessage = "Contest Media Create successfully!",
                        BoolData = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Contest Media Create Failed!",
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
        [HttpPut("{contestId:int}/Media/{pictureId:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ContestMediaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMeetingMedia(
            [Required][FromRoute] int contestId,
            [Required][FromRoute] int pictureId,
            [Required][FromBody] ContestMediaViewModel media)
        {
            try
            {
                var check = _contestService.GetById(contestId).Result;
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest does not exist!"
                });
                var pic = await _mediaService.GetById(pictureId);
                if (pic == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest Media does not exist!"
                });
                var result = await _mediaService.Update(contestId, media);
                if (result) return Ok(new
                {
                    Status = true,
                    BoolData = result
                });
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest does not exist or internal server error"
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
    }
}
