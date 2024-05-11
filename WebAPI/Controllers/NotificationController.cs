using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMemberService _memberService;
        private readonly IConfiguration _config;
        public NotificationController(INotificationService notificationService, IUserService userService, IMemberService memberService, IConfiguration config)
        {
            _notificationService = notificationService;
            _userService = userService;
            _memberService = memberService;
            _config = config;
        }

        [HttpPost("AllNotifications")]
        [ProducesResponseType(typeof(List<NotificationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationByUserId([FromBody] int id)
        {
            try
            {
                var mem = await _userService.GetBoolById(id);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"
                });
                var result = await _notificationService.GetAllNotificationsByUserId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Notifications Found!"
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

        [HttpPost("{id:int}/Create")]
        [ProducesResponseType(typeof(NotificationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotification(
            [Required][FromRoute] int id,
            [Required][FromBody] NotificationViewModel notif)
        {
            try
            {
                if (await _notificationService.Create(id, notif))
                    return Ok(new
                    {
                        Status = true,
                        Message = "New notification created!",
                        Data = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Notification Create Failed!",
                    Data = false
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
        [ProducesResponseType(typeof(IEnumerable<NotificationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllNotificationStatus(
            [Required][FromRoute] int id,
            [Required][FromBody] List<NotificationViewModel> listNotif)
        {
            try
            {
                var check = await _notificationService.GetBoolNotificationId(id);
                if (!check) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Notification does not exist"
                });
                var result = await _notificationService.UpdateAllNotificationStatus(listNotif);
                if (!result) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "All Notification Status Update Failed"
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
        [HttpPost("Count")]
        [ProducesResponseType(typeof(NotificationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCountUnreadNotificationsByMemberId([FromBody] string id)
        {
            try
            {
                var mem = await _memberService.GetBoolById(id);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found"
                });
                var result = await _notificationService.GetCountUnreadNotificationsByMemberId(id);
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
