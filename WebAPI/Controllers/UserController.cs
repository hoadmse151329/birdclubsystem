using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebAPI.Utilities;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using BAL.ViewModels.Member;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        //private readonly OtpGenerator otpver = new OtpGenerator(6);
        private readonly OtpGenerator otpname = new OtpGenerator(10);

        public UserController(IUserService userService,IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        /// <summary>
        /// Get user informations by User ID
        /// </summary>
        ///      <param name="id">User's Account ID</param>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserById([FromRoute] int id)
        {
            try
            {
                var result = _userService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "User Not Found!"
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
        /// Login, get user informations by User's Email and Password
        /// </summary>
        ///      <param name="email">User's Email</param>
        ///      <param name="password">User's Password</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET 
        ///     {
        ///         "email": "example123@gmail.com",
        ///         "password": "example123"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserLogin(
            [FromBody] AuthenRequest loguser)
        {
            try
            {
                var result = await _userService.AuthenticateUser(loguser);
                if (result == null)
                {
                    return NotFound(new
                    {
						Status = false,
						ErrorMessage = "User Not Found!"
                    });
                }
				if (result.Status == "Inactive")
				{
					return BadRequest(new
					{
						Status = false,
						ErrorMessage = "User Account is Currently InActivated!"
					});
				}
                if (result.Status == "Expired")
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "User Account is Currently Expired!"
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
        /// Login (Register if not found) by User's Email, use for Google Login
        /// </summary>
        /// <param name="email">User's Email</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET 
        ///     {
        ///         "email": "example123@gmail.com"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        [HttpPost("LoginByThirdParty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserLoginByThirdParty(
            [FromBody][Required][EmailAddress][DataType(DataType.EmailAddress)] string email)
        {
            try
            {
                var result = await _userService.GetByEmailModel(email);
                if (result == null)
                {
                    // Handle the case where user is not found
                    return NotFound(new { ErrorMessage = "User not found." });
                }

                var loguser = new AuthenRequest()
                {
                    Username = result.UserName,
                    Password = null // No need to specify password
                };
                var login = _userService.AuthenticateUser(loguser);
                return Ok(new
                {
                    Status = true,
                    Data = login
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
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(
            [FromBody][Required] CreateNewMember newmem)
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
                var result = await _userService.GetByEmailModel(newmem.Email);
                if (result != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
						ErrorMessage = "Email has already registered !"
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
                    Email= newmem.Email,
                    Password= newmem.Password,
                    Role = "Member",
                    ImagePath = "https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png"
                };
                _userService.Create(value,newmem);
                var loguser = new AuthenRequest()
                {
                    Username = newmem.UserName,
                    Password = newmem.Password,
                    ImagePath = value.ImagePath
                };
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
		[HttpPost("RegisterTempMember")]
		[ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateTempUser(
			[FromBody][Required] CreateNewMember newmem)
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
				var result = await _userService.GetByEmailModel(newmem.Email);
				if (result != null)
				{
					return BadRequest(new
					{
						Status = false,
						ErrorMessage = "Email has already registered !"
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
				var loguser = new AuthenRequest()
				{
					Username = newmem.UserName,
					Password = newmem.Password
				};
				var resultaft = await _userService.CreateTemporaryNewUser(loguser);

				if (resultaft == null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError, new
					{
						Status = false,
						ErrorMessage = "Error while Registering your Temp Account !"

					});
				}
				return Ok(new
				{
					Status = true,
					SuccessMessage = "Temp Account Create successfully !",
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
		// PUT api/<UserController>/5
		// PUT api/<UserController>/Update/5
		/// <summary>
		/// Update User Account informations by ID
		/// aliases: api/User/{id} or api/User/Update/{id}
		/// </summary>
		/// <param name="id">Account ID</param>
		/// <remarks>
		/// Sample request:
		/// 
		///     PUT 
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
		[HttpPut("{id}")]
        [Authorize(Roles ="Admin,Member")]
        [HttpPut("Update/{id}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromForm][Required][EmailAddress] string email,
            [FromForm][Required] string username,
            [FromForm][Required] string role)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account does not exist !"
                    });
                }
                result.Email = email;
                result.UserName = username;
                result.Role = role;
                _userService.Update(result);
                result = await _userService.GetById(id);
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
        // PUT api/<UserController>/ChangePassword
        /// <summary>
        /// Change Account Password
        /// </summary>
        /// <param name="email">Account Email</param>
        /// <param name="password">Old Password</param>
        /// <param name="Newpassword">New Password</param>
        /// <param name="NewConfirmPassword">Confirm New Password</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT 
        ///     {
        ///         "email": "example123@gmail.com",
        ///         "password": "example123",
        ///         "newPassword": "example123456",
        ///         "confirmNewPassword": "example123456"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        [HttpPut("ChangePassword")]
        [Authorize(Roles ="Admin,Manager,Staff,Member")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromBody][Required] UpdateMemberPassword upPass)
        {
            try
            {
                var result = await _userService.GetByMemberId(upPass.userId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
						ErrorMessage = "Account does not exist !"
                    });
                }
                if (!upPass.Newpassword.Equals(upPass.NewConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = true,
						ErrorMessage = "New Password and New Confirm Password are not the same !"
                    });
                }
                result.Password = upPass.Newpassword;
                _userService.UpdatePassword(result);
                return Ok(new
                {
					Status = true,
                    Data = true
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
        [HttpPost("Upload")]
        //[Authorize(Roles = "Admin,Member")]
        [ProducesResponseType(typeof(UpdateMemberAvatar), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(
            [FromBody][Required] UpdateMemberAvatar uploadmem)
        {
            try
            {
                var result = await _userService.UpdateUserAvatar(uploadmem.MemberId, uploadmem.ImagePath);
                if (!result) throw new Exception("Image Update Error !");
                return Ok(new
                {
                    Status = true,
                    Data = uploadmem
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

        [HttpGet("GetId")]
        [Authorize(Roles = "Admin,Member")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIdByUsername(
            [FromQuery][Required] string username)
        {
            try
            {
                var result = await _userService.GetIdByUsername(username);
                if (result == 0)
                {
                    throw new Exception("Member does not exist!");
                }
                return Ok(new
                {
                    Status = true,
                    result
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
        #region old code reset password
        /*// PUT api/<UserController>/5
        /// <summary>
        /// Reset Account Password, requires Email OTP Verification
        /// </summary>
        /// <param name="email">Account Email</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT 
        ///     {
        ///         "email": "example123@gmail.com"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPassword(
            [Required][EmailAddress] string email)
        {
            try
            {
                var result = _userRepo.GetByEmail(email);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Account does not exist !"
                    });
                }
                string sRanOtp = otpver.GenerateRandomOTP();
                MailUtilities.SendMailGoogleSmtp("letsbirdclub@gmail.com",
                    email,
                    "CC ChaoMao Bird Club Customer Service ",
                    $"<h1>Reset Password request: Hello User {result.Username}! </h1>" +
                    "<p>Your verification code is <" + sRanOtp + "> .</p>" +
                    "This Otp is only valid for 2 minutes. Have a great day!</p>",
                    "letsbirdclub@gmail.com").Wait();
                _userRepo.Update(result,
                        ForgotOTP: sRanOtp,
                        ForgotOTPCreatedDate: DateTime.Now);
                return Ok(new
                {
                    status = true,
                    message = $"We have send an Account Verification OTP for Your Reset Password request to your Email {email} !",
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
        }*/
        #endregion
        #region old code verify otp
        /*/// <summary>
        /// Verifying User's Account by OTP for Reset Password request.
        /// </summary>
        /// <param name="otp">OTP for Account Verification (Get OTP from User's Email)</param>
        /// <param name="Newpassword">New Default Password</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET 
        ///     {
        ///         "otp": "123456",
        ///         "Newpassword": "example123456"
        ///     } 
        ///     
        /// </remarks>
        /// <returns>Return result of action and error message</returns>
        [HttpPost("ResetPasswordOTP")]
        //[ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPasswordOTP(
            [Required][StringLength(6)] string otp,
            [Required][PasswordPropertyText] string Newpassword
            )
        {
            try
            {
                var result = _userRepo.GetByForgotOtp(otp);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Invalid OTP !"
                    });
                }
                DateTime forgotdate = result.ForgotOtpCreatedDate.Value.AddMinutes(2);
                if (DateTime.Now.CompareTo(forgotdate) >= 0)
                {
                    string sRanOtp = otpver.GenerateRandomOTP();
                    MailUtilities.SendMailGoogleSmtp("letsbirdclub@gmail.com",
                        result.Email,
                        "CC ChaoMao Bird Club Customer Service ",
                        $"<h1>New OTP for Reset Password request: Hello User {result.Username}! </h1>" +
                        "<p>Your verification code is <" + sRanOtp + "> .</p>" +
                        "This Otp is only valid for 2 minutes. Have a great day!</p>",
                        "letsbirdclub@gmail.com").Wait();
                    _userRepo.Update(result,
                        ForgotOTP: sRanOtp,
                        ForgotOTPCreatedDate: DateTime.Now);
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "OTP Expired, Please try again with a new OTP we just send you ! " +
                        $"Please check your email {result.Email} to get New Verfication Code for Reset Password Request !"
                    });
                }
                result.Password = Newpassword;
                _userRepo.Update(result);
                result = _userRepo.GetByLogin(result.Email, result.Password);
                return Ok(new
                {
                    status = true,
                    message = "Your Account Default Password has been changed successfully !",
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
        }*/
        #endregion
    }
}
