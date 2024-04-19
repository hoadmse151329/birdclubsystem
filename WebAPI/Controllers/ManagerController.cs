﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Models; // Replace with the actual namespace of your models
using DAL.Repositories;
using DAL.Repositories.Implements;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BAL.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        /*private const string ERROR = "~/Home/Error";
        private const string SUCCESS = "~/Manager/ManagerProfile";*/
        private readonly IMemberService _memberService;
        private readonly IConfiguration _config;

        public ManagerController(IMemberService memberService, IConfiguration config)
        {
            _memberService = memberService;
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
    }
}
