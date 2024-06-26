﻿using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Manager;
using BAL.ViewModels.Member;
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
        public async Task<IActionResult> GetListofMembers([FromQuery] string? memberUsername)
        {
            try
            {
                var result = await _memberService.GetSortedMembers(memberUserName: memberUsername, isManagerGetMemberList: true);
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
        public async Task<IActionResult> UpdateMemberStatus(
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
                        ErrorMessage = "Member details not found"
                    });
                }
                if (member.Status == null)
                {
                    member.Status = result.Status;
                }
                _memberService.UpdateMemberStatus(member);
                result = await _memberService.GetById(member.MemberId);
                if (result.Status.Equals(member.Status))
                {
                    return Ok(new
                    {
                        Status = true,
                        Data = result
                    });
                }
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = "Member status not updated"
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

        [HttpPost("MemberName")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMemberNameById([FromBody] string id)
        {
            try
            {
                var result = await _memberService.GetMemberNameById(id);
                if (result == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Full Name Not Found!"
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
        [HttpPut("RenewMembership")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RenewMembership([FromBody] string id)
        {
            try
            {
                var check = await _memberService.GetById(id);
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member not Found!"
                });
                if (check.Status != "Expired") return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = "Member is not yet Expired!"
                });
                _memberService.RenewMembership(id);
                var result = await _memberService.GetById(id);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully renewed Membership!",
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
