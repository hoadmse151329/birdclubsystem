using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebAPI.Utilities;
using DAL.Repositories.Interfaces;
using DAL.Models;

namespace WebAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly OtpGenerator otpver = new OtpGenerator(6);
        private readonly OtpGenerator otpname = new OtpGenerator(10);

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Get user informations by User ID
        /// </summary>
        ///      <param name="id">User's Account ID</param>
        /// <returns>Return result of action and error message</returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserById([FromRoute] int id)
        {
            try
            {
                var result = _userRepo.GetById(id);
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserLogin([FromForm][Required] string userName, [FromForm][Required][PasswordPropertyText] string password)
        {
            try
            {
                var result = _userRepo.GetByLogin(userName, password);
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
            [Required][EmailAddress] string email)
        {
            try
            {
                var result = _userRepo.GetByEmail(email);
                if (result == null)
                {
                    string sRanName = otpname.GenerateRandomOTP();
                    string sRanPassword = otpname.GenerateRandomOTP();
                    var usr = new User()
                    {
                        UserName = "NewGoogleUser" + sRanName,
                        Password = sRanPassword,
                    };
                    usr.Member = new Member();
                    usr.Member.FullName = "NewGoogleUser" + sRanName;
                    usr.Member.Email = email;
                    _userRepo.Insert(usr);
                    usr = _userRepo.GetByEmail(email);
                    return Ok(new
                    {
                        status = true,
                        usr,
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser(
            [FromForm] [Required] string username,
            [FromForm] [Required] [PasswordPropertyText] string password,
            [FromForm] [Required] [PasswordPropertyText] string confirmPassword,
            [FromForm] [Required] [EmailAddress(ErrorMessage ="Email is Invalid !")] string email)
        {
            try
            {
                var value = new User()
                {
                    UserName = username,
                    Password = password
                };
                if (value.Password == null || value.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Password is Empty !"
                    });
                }
                var result = _userRepo.GetByEmail(email);
                if (result != null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Email has already registered !"
                    });
                }
                if (!value.Password.Equals(confirmPassword))
                {
                    return BadRequest(new
                    {
                        status = false,
                        errorMessage = "Password and Confirm Password are not the same !"
                    });
                }
                value.Member = new Member();
                value.Member.Email = email;
                _userRepo.Insert(value);
                _userRepo.Save();

                var resultaft = _userRepo.GetByLogin(value.UserName, value.Password);

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
        [HttpPut("Update/{id}")]
        //[ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute][Required] int id, 
            [FromBody] string userName,
            [FromBody] string passWord)
        {
            try
            {
                var value = new User()
                {
                    UserName = userName,
                    Password = passWord
                };
                value.UserId = id;
                var result = _userRepo.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Account does not exist !"
                    });
                }
                _userRepo.Update(value/*,
                        ForgotOTP: value.ForgotOtp,
                        ForgotOTPCreatedDate: value.ForgotOtpCreatedDate*/);
                result = _userRepo.GetById(value.UserId);
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
        //[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword(
            [Required][EmailAddress] string email,
            [Required][PasswordPropertyText] string password,
            [Required][PasswordPropertyText] string Newpassword,
            [Required][PasswordPropertyText] string NewConfirmPassword)
        {
            try
            {
                var result = _userRepo.GetByLogin(email, password);
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
                _userRepo.Update(result);
                result = _userRepo.GetById(result.UserId);
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
