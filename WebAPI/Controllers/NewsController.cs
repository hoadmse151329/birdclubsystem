using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("All")]
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
    }
}
