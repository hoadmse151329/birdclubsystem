using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebAPI.Utilities;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
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
                        status = false,
                        errorMessage = "User Not Found!"
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
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserLogin(
            [FromForm][Required] string userName, 
            [FromForm][Required][PasswordPropertyText][DataType(DataType.Password)] string password)
        {
            try
            {
                var loguser = new AuthenRequest()
                {
                    Username = userName,
                    Password = password
                };
                var result = _userService.AuthenticateUser(loguser);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "User Not Found!"
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
        public IActionResult GetUserLoginByThirdParty(
            [FromBody][Required][EmailAddress][DataType(DataType.EmailAddress)] string email)
        {
            try
            {
                var result = _userService.GetByEmailModel(email);
                if (result == null)
                {
                    string sRanName = otpname.GenerateRandomOTP();
                    string sRanPassword = otpname.GenerateRandomOTP();
                    var usr = new UserViewModel()
                    {
                        UserName = "NewGoogleUser" + sRanName,
                        Password = sRanPassword,
                        Email = email
                    };
                    _userService.Create(usr);
                    result = _userService.GetByEmailModel(email);
                }
                var loguser = new AuthenRequest()
                {
                    Username = result.UserName,
                    Password = result.Password
                };
                var login = _userService.AuthenticateUser(loguser);
                return Ok(new
                {
                    status = true,
                    login
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
        // POST api/<UserController>
        // POST api/<UserController>/Register
        /// <summary>
        /// Register New User Account
        /// aliases: api/User/ or api/User/Register
        /// </summary>
        /// <param name="value">Object Type: UserViewModel</param>
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
        public IActionResult CreateUser(
            [FromForm] [Required] string username,
            [FromForm] [Required] [EmailAddress] [DataType(DataType.EmailAddress)] string email,
            [FromForm] [Required] [PasswordPropertyText] [DataType(DataType.Password)] string password,
            [FromForm] [Required] [PasswordPropertyText] [DataType(DataType.Password)] string confirmPassword)
        {
            try
            {
                if (password == null || password == string.Empty)
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Password is Empty !"
                    });
                }
                var result = _userService.GetByEmailModel(email);
                if (result != null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Email has already registered !"
                    });
                }
                if (!password.Equals(confirmPassword))
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Password and Confirm Password are not the same !"
                    });
                }
                UserViewModel value = new UserViewModel()
                {
                    UserName= username,
                    Email= email,
                    Password= password,
                };
                _userService.Create(value);
                var loguser = new AuthenRequest()
                {
                    Username = username,
                    Password = password
                };
                var resultaft = _userService.AuthenticateUser(loguser);

                if (resultaft == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        status = false,
                        errorMessage = "Error while Registering your Account !"

                    });
                }
                return Ok(new
                {
                    status = true,
                    Message = "Account Create successfully !",
                    resultaft
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
        // PUT api/<UserController>/5
        // PUT api/<UserController>/Update/5
        /// <summary>
        /// Update User Account informations by ID
        /// aliases: api/User/{id} or api/User/Update/{id}
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="value">Object Type: UserViewModel</param>
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
        public IActionResult Update(
            [FromRoute][Required] int id,
            [FromForm][Required][EmailAddress] string email,
            [FromForm][Required] string username)
        {
            try
            {
                var result = _userService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Account does not exist !"
                    });
                }
                result.Email = email;
                result.UserName = username;
                _userService.Update(result);
                result = _userService.GetById(id);
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
        [Authorize(Roles ="Admin,Member")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword(
            [FromForm][Required][EmailAddress] string email,
            [FromForm][Required][PasswordPropertyText] string password,
            [FromForm][Required][PasswordPropertyText] string Newpassword,
            [FromForm][Required][PasswordPropertyText] string NewConfirmPassword)
        {
            try
            {
                var result = _userService.GetByEmailModel(email);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Account does not exist !"
                    });
                }
                if (!Newpassword.Equals(NewConfirmPassword))
                {
                    return BadRequest(new
                    {
                        status = true,
                        errorMessage = "New Password and New Confirm Password are not the same !"
                    });
                }
                result.Password = Newpassword;
                _userService.Update(result);
                result = _userService.GetById(result.UserId.Value);
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
    }
}
