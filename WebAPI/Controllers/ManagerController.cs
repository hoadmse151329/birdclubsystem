using Microsoft.AspNetCore.Mvc;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BAL.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using BAL.ViewModels.Manager;
using BAL.Services.Implements;
using BAL.ViewModels.Event;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        /*private const string ERROR = "~/Home/Error";
        private const string SUCCESS = "~/Manager/ManagerProfile";*/
        private readonly IMemberService _memberService;
        private readonly IConfiguration _config;
        private readonly IMeetingService _meetingService;
        private readonly IFieldTripService _fieldTripService;
        private readonly IContestService _contestService;
        private readonly IFeedbackService _feedbackService;
        private readonly IBlogService _blogService;
        private readonly INewsService _newsService;
        private readonly ITransactionService _transactionService;
        private readonly IContestParticipantService _contestParticipantService;

        public ManagerController(
            IMemberService memberService,
            IMeetingService meetingService,
            IFieldTripService fieldTripService,
            IContestService contestService,
            IFeedbackService feedbackService,
            IBlogService blogService,
            INewsService newsService,
            ITransactionService transactionService,
            IContestParticipantService contestParticipantService,
            IConfiguration config)
        {
            _memberService = memberService;
            _meetingService = meetingService;
            _fieldTripService = fieldTripService;
            _contestService = contestService;
            _feedbackService = feedbackService;
            _blogService = blogService;
            _newsService = newsService;
            _transactionService = transactionService;
            _contestParticipantService = contestParticipantService;
            _config = config;
        }
        #region old Upload Image Code
        /*[HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile photo, string userID)
        {
            try
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(pathImage, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    var image = "images/" + fileName;

                    // Replace "UserManager" with the actual class managing users
                    var userManager = new UserManager(); // Replace with the actual class managing users
                    var check = userManager.ChangeImage(userID, image);

                    if (check != null)
                    {
                        return Ok(new { Message = "Image uploaded successfully", RedirectUrl = SUCCESS });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return BadRequest(new { Message = "Image upload failed", RedirectUrl = ERROR });
        }*/
        #endregion

        [HttpGet("Index")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(GetDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManagerDashboard()
        {
            try
            {
                int eventCount = await _meetingService.CountMeeting() + await _fieldTripService.CountFieldTrip() + await _contestService.CountContest();
                int feedbackCount = await _feedbackService.CountFeedback();
                int blogCount = await _blogService.CountBlog();
                int newsCount = await _newsService.CountNews();
                int totalValue = await _transactionService.CalculateTotalValue();
                GetDashboardResponse result = new GetDashboardResponse()
                {
                    TotalEvents = eventCount,
                    TotalFeedbacks = feedbackCount,
                    TotalBlogs = blogCount,
                    TotalNews = newsCount,
                    TotalIncome = totalValue,
                };
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
        ///      <param name="id">Member's Details ID</param>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpPost("Profile")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManagerDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Manager Details Not Found!"
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
        [HttpGet("MemberStatus")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(IEnumerable<GetMemberStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMemberStatus(
            [FromQuery] string? memberusername
            )
        {
            try
            {
                var result = await _memberService.GetSortedMembers(memberUserName: memberusername, isManager: true);
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
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(IEnumerable<GetMemberStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllMemberStatus([Required][FromBody] List<GetMemberStatus> listMem)
        {
            try
            {
                var result = await _memberService.UpdateAllMemberStatus(listMem);
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
                    SuccessMessage = "Successfully update Member Status!",
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
        [HttpPut("Contest/{id:int}/Participant/All/Score/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(IEnumerable<ContestParticipantViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllContestParticipantScoreLast(
            [Required][FromRoute] int id,
            [Required][FromBody] List<ContestParticipantViewModel> listPart)
        {
            try
            {
                var check = await _contestService.GetById(id);
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest does not exist"
                });
                if (!check.Status.Equals("Ended")) NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Contest status is not \"Ended\" to use this feature"
                });
                var result = await _contestParticipantService.UpdateAllContestParticipantScore(listPart, true);
                if (!result) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "All Contest Participant Score Update Failed"
                });
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Contest Score!",
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
        /// <summary>
        /// Get member informations by Member ID
        /// </summary>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpPut("Profile/Update")]
        [Authorize(Roles = "Manager,Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateManagerDetails(
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
                        ErrorMessage = "Manager Details Not Found!"
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
                if(member.UserId == 0)
                {
                    member.UserId = result.UserId;
                }
                _memberService.Update(member);
                result = await _memberService.GetById(member.MemberId);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully update Profile!",
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
