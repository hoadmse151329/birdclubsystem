using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MemberController : ControllerBase
	{
		private readonly IMemberService _memberService;
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
		public IActionResult GetMemberDetailsById([FromRoute] int id)
		{
			try
			{
				var result = _memberService.GetById(id);
				if (result == null)
				{
					return NotFound(new
					{
						status = false,
						errorMessage = "Member Details Not Found!"
					});
				}

				return Ok(new
				{
					status = true,
					result
				});
			}
			catch (Exception ex)
			{
				// Log the exception if needed
				return BadRequest(new
				{
					status = false,
					errorMessage = ex.Message
				});
			}
		}
		/// <summary>
		/// Get member informations by Member ID
		/// </summary>
		///      <param name="id">Member's Details ID</param>
		/// <returns>Return result of action and error message</returns>
		// GET api/<UserController>/5
		[HttpPut("{id}")]
		[Authorize(Roles = "Member,Admin")]
		[HttpPut("Update/{id}")]
		[ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateMemberDetails(
			//[FromBody] int memId,
			[FromBody] MemberViewModel member
			)
		{
			try
			{
				var result = await _memberService.GetById(member.MemberId.Value);
				if (result == null)
				{
					return NotFound(new
					{
						status = false,
						errorMessage = "Member Details Not Found!"
					});
				}
				_memberService.Update(member);
				result = await _memberService.GetById(member.MemberId.Value);
				return Ok(new
				{
					status = true,
					result
				});
			}
			catch (Exception ex)
			{
				// Log the exception if needed
				return BadRequest(new
				{
					status = false,
					errorMessage = ex.Message
				});
			}
		}
	}
}
