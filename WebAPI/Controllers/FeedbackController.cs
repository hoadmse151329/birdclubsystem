using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
using BAL.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public FeedbackController(IFeedbackService feedbackService, IUserService userService, IConfiguration config)
        {
            _feedbackService = feedbackService;
            _userService = userService;
            _config = config;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(List<GetFeedbackResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var result = await _feedbackService.GetAllFeedbacks();
                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Feedbacks Found!"
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
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(FeedbackViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFeedback(
            [Required][FromBody] CreateFeedbackRequest feedback)
        {
            try
            {
                var usr = await _userService.GetByMemberId(feedback.MemberId);
                if (usr == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "User does not exist!"
                });
                FeedbackViewModel feed = new FeedbackViewModel()
                {
                    UserId = usr.UserId,
                    EventId = feedback.EventId,
                    Title = feedback.Title,
                    Details = feedback.Details,
                    Category = feedback.Category,
                    Rating = feedback.Rating,
                    Date = DateTime.Now,
                    Status = "Unread"
                };
                _feedbackService.Create(feed);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Feedback Create successfully!",
                    Data = feed
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
