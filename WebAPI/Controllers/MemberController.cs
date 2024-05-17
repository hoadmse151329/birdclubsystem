using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MemberController : ControllerBase
	{
		private readonly IMemberService _memberService;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _config;

		public MemberController(IMemberService memberService, IConfiguration config)
		{
			_memberService = memberService;
			_config = config;
		}
		/// <summary>
		/// Get member informations by Member ID
		/// </summary>
		///      <param name="id">Member's Details ID</param>
		/// <returns>Return result of action and error message</returns>
		// GET api/<UserController>/5
		[HttpGet("{id}")]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetMemberDetailsById([FromRoute] string id)
		{
			try
			{
				var result = await _memberService.GetById(id);
				if (result == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "Member Details Not Found!"
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
        [HttpGet("All/Role/Member")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(List<GetMembershipExpire>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListofMembers()
        {
            try
            {
                var result = await _memberService.GetAllMemberStatusWithExpireByRole("Member");
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Member Details Not Found!"
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
        [HttpPut("Update/Status")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMemberDetails(
            [Required][FromBody] GetMembershipExpire member
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
                        ErrorMessage = "Member Details Not Found!"
                    });
                }
                if (member.Status == null)
                {
                    member.Status = result.Status;
                }
                _memberService.UpdateMemberStatus(member);
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
        /// <summary>
        /// Get member informations by Member ID
        /// </summary>
        ///      <param name="id">Member's Details ID</param>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpPost("Profile")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMemberDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Member Details Not Found!"
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
        [HttpPut("Update")]
        [Authorize(Roles = "Member,Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateMemberDetails(
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
						ErrorMessage = "Member Details Not Found!"
					});
				}
                if(member.Status == null)
                {
					member.Status = result.Status;
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
        [HttpPost("AllNotifications")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<NotificationViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllNotificationsByUserId([Required][FromBody] int id)
        {
            try
            {
                var result = await _notificationService.GetAllNotificationsByUserId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Notifications Not Found!"
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
        [HttpPost("Feedback/Create")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(FeedbackViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackViewModel feedback)
        {
            try
            {
                return null;
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
