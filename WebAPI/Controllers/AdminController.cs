using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BAL.ViewModels.Admin;
using BAL.Services.Implements;
using BAL.ViewModels.Authenticates;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemberService _memberService;
        private readonly IConfiguration _config;
        private readonly IContestService _contestService;
        private readonly IContestParticipantService _contestParticipantService;

        public AdminController(
            IUserService userService,
            IMemberService memberService, 
            IContestService contestService, 
            IContestParticipantService contestParticipantService, 
            IConfiguration config)
        {
            _userService = userService;
            _memberService = memberService;
            _contestService = contestService;
            _contestParticipantService = contestParticipantService;
            _config = config;
        }
        [HttpPost("Profile")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdminDetailsByUsrId([FromBody] string memId)
        {
            try
            {
                var result = await _memberService.GetById(memId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Admin Details Not Found!"
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(MemberViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdminDetails(
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
                        ErrorMessage = "Admin Details Not Found!"
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
                if (member.UserId == 0)
                {
                    member.UserId = result.UserId;
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
        [HttpGet("MemberStatus")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<GetEmployeeStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMemberStatus(
            [FromQuery] string? memberusername
            )
        {
            try
            {
                var result = await _memberService.GetSortedEmployees(memberUserName: memberusername, isAdmin: true);
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<GetEmployeeStatus>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAllMemberStatus([Required][FromBody] List<GetEmployeeStatus> listMem)
        {
            try
            {
                var result = await _memberService.UpdateAllEmployeeStatus(listMem);
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
        // POST api/<UserController>
        // POST api/<UserController>/Register
        /// <summary>
        /// Register New User Account
        /// aliases: api/User/ or api/User/Register
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST 
        ///     {
        ///         "id": 1,
        ///         "username": "ExampleMan123",
        ///         "password": "example123",
        ///         "confirmPassword": "example123",
        ///         "fullName": "Mr. ExampleMan",
        ///         "email": "example123@gmail.com",
        ///         "phone": "0123456789",
        ///         "address": "123, Brooklyn, New York City, USA"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        //[HttpPost]
        [HttpPost("User/Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserByAdmin(
            [FromBody][Required] CreateNewEmployee newmem)
        {
            try
            {
                if (newmem.Password == null || newmem.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty !"
                    });
                }
                var resultEmail = await _userService.IsUserExistByEmail(newmem.Email);
                if (resultEmail)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Email has already registered !"
                    });
                }
                var resultUsername = await _userService.IsUserExistByUsername(newmem.UserName);
                if (resultUsername)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Username has already been taken, please type in a different Username !"
                    });
                }
                if (!newmem.Password.Equals(newmem.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password are not the same !"
                    });
                }
                UserViewModel value = new UserViewModel()
                {
                    UserName = newmem.UserName,
                    Email = newmem.Email,
                    Password = newmem.Password,
                    Role = newmem.Role,
                };
                _userService.Create(value, newmem);
                var loguser = new AuthenRequest(
                    userName: newmem.UserName, 
                    passWord: newmem.Password, 
                    imagePath: value.ImagePath
                    );
                var resultaft = await _userService.AuthenticateUser(loguser);

                if (resultaft == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = false,
                        ErrorMessage = "Error while Registering your Account !"

                    });
                }
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Account Create successfully !",
                    Data = resultaft
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
