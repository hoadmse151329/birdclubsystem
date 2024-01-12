﻿using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
		public IActionResult GetMemberDetailsById([FromRoute] string id)
		{
			try
			{
				var result = _memberService.GetById(id);
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
