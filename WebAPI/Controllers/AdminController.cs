using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BAL.ViewModels.Admin;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IConfiguration _config;
        private readonly IContestService _contestService;
        private readonly IContestParticipantService _contestParticipantService;

        public AdminController(IMemberService memberService, IContestService contestService, IContestParticipantService contestParticipantService, IConfiguration config)
        {
            _memberService = memberService;
            _contestService = contestService;
            _contestParticipantService = contestParticipantService;
            _config = config;
        }
        [HttpPost("Profile")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdminDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Admin Details Not Found!"
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
        /// <summary>
        /// Get member informations by Member ID
        /// </summary>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpPut("Profile/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdminDetails(
            [Required][FromBody] MemberViewModel member
            )
        {
            try
            {
                var result = await _memberService.GetById(member.MemberId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Admin Details Not Found!"
                    });
                }
                if (member.Status == null)
                {
                    member.Status = result.Status;
                }
                if (member.ImagePath == null)
                {
                    member.ImagePath = result.ImagePath;
                }
                if (member.UserId == 0)
                {
                    member.UserId = result.UserId;
                }
                _memberService.Update(member);
                result = await _memberService.GetById(member.MemberId);
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
        [HttpGet("MemberStatus")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<GetEmployeeStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMemberStatus(
            [FromQuery] string? memberusername
            )
        {
            try
            {
                var result = await _memberService.GetSortedEmployees(memberUserName: memberusername, isAdmin: true);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "All Member Status Not Found!"
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
        [HttpPut("MemberStatus/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<GetEmployeeStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllMemberStatus([Required][FromBody] List<GetEmployeeStatus> listMem)
        {
            try
            {
                var result = await _memberService.UpdateAllEmployeeStatus(listMem);
                if (!result)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "All Member Status Updating failed!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    BoolData = result
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
