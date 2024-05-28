using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public NewsController(IUserService userService, IConfiguration config, INewsService newsService)
        {
            _userService = userService;
            _config = config;
            _newsService = newsService;
        }
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<NewsViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllNews(
            )
        {
            try
            {
                var result = await _newsService.GetAllNews();
                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of News Not Found!"
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPost("Search")]
        [ProducesResponseType(typeof(List<NewsViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeetingsByAttributes(
            [FromBody] string? role,
            [FromQuery] string? title,
            [FromQuery] string? userName,
            [FromQuery] List<string>? category,
            [FromQuery] DateTime? uploadDate,
            [FromQuery] List<string>? status,
            [FromQuery] string? orderBy)
        {
            try
            {
                bool isMemberOrGuest = false;
                if (!string.IsNullOrEmpty(role) && (role.Equals("Member") || role.Equals("Guest")))
                {
                    isMemberOrGuest = true;
                }
                int? usrID = null;
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrWhiteSpace(userName))
                {
                    var usrIDresult = await _userService.GetIdByUsername(userName);
                    if (usrIDresult > 0)
                    {
                        usrID = usrIDresult;
                    }
                }
                var result = await _newsService.GetSortedNews(
                    title: title,
                    categories: category,
                    uploadDate: uploadDate,
                    statuses: status,
                    orderBy: orderBy,
                    userId: usrID,
                    isMemberOrGuest: isMemberOrGuest);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of News Not Found!"
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
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NewsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNewsById(
            [FromRoute] int id)
        {
            try
            {
                var result = await _newsService.GetNewsByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "News Not Found!"
                    });
                }

                return Ok(new
                {
                    status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [Required][FromBody] CreateNewNews newsVM)
        {
            try
            {
                var isCreated = await _newsService.Create(newsVM);
                if (isCreated)
                {
                    return Ok(new
                    {
                        Status = true,
                        SuccessMessage = "News Create successfully !",
                        Data = newsVM
                    });
                }
                return NotFound(new
                {
                    status = false,
                    errorMessage = "User Id Not Found!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpPut("{id:int}/Update")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [FromBody] NewsViewModel newsVM)
        {
            try
            {
                var result = await _newsService.GetNewsByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "News does not exist!"
                    });
                }
                newsVM.NewsId = id;
                _newsService.Update(newsVM);
                result = await _newsService.GetNewsByIdNoTracking(newsVM.NewsId.Value);
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
        [HttpGet("{id:int}/Disable")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(NewsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCancelMeeting(
            [Required][FromRoute] int id)
        {
            try
            {
                var result = await _newsService.GetNewsByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "News post does not exist!"
                    });
                }
                result.NewsId = id;
                result.Status = "Cancelled";
                _newsService.Update(result);
                result = await _newsService.GetNewsByIdNoTracking(id);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully Disabled News post!",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }
    }
}
