using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Event;
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

        [HttpPost("CreateRegister")]
        [ProducesResponseType(typeof(NotificationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotificationRegister(
            [Required][FromBody] NotificationViewModel notif)
        {
            try
            {
                _notificationService.Create(notif);
                var result = await _notificationService.GetNotificationById(notif.NotificationId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Notification Create Failed!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Message = "Notification Create successfully !",
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

        [HttpPost("CreateEvent")]
        [ProducesResponseType(typeof(NotificationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotificationEvent(
            [Required][FromBody] CreateNotificationRequest notif)
        {
            try
            {
                var usr = await _userService.GetByMemberId(notif.MemberId);
                if (usr == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "User does not exist!"
                    });
                }
                NotificationViewModel notification = new NotificationViewModel()
                {
                    NotificationId = Guid.NewGuid().ToString(),
                    Title = notif.Title,
                    Description = notif.Description,
                    Date = DateTime.Now,
                    UserId = usr.UserId,
                    Status = "Unread"
                };
                _notificationService.Create(notification);
                var result = await _notificationService.GetNotificationById(notification.NotificationId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Notification Create Failed!",
                        BoolData = true
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Message = "Notification Create successfully !",
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

        [HttpPut("{id:int}/Update")]
        [ProducesResponseType(typeof(IEnumerable<NotificationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllNotificationStatus(
            [Required][FromRoute] string id,
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
                    IntData = result
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
        [HttpPost("Unread")]
        [ProducesResponseType(typeof(NotificationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUnreadNotificationTitle([FromBody] string id)
        {
            try
            {
                var mem = await _memberService.GetBoolById(id);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found"
                });
                var result = await _notificationService.GetUnreadNotificationTitle(id);
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
