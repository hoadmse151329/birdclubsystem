using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public NotificationController(INotificationService notificationService, IUserService userService ,IConfiguration config)
        {
            _notificationService = notificationService;
            _userService = userService;
            _config = config;
        }

        [HttpPost("AllNotifications")]
        [ProducesResponseType(typeof(List<NotificationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationByUserId([FromBody] int userId)
        {
            try
            {
                var usr = await _userService.GetBoolById(userId);
                if (!usr) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "User Not Found!"
                });
                var result = await _notificationService.GetAllNotificationsByUserId(userId);
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
    }
}
