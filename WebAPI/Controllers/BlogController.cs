using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public BlogController(IUserService userService, IConfiguration config, IBlogService blogService)
        {
            _userService = userService;
            _config = config;
            _blogService = blogService;
        }
        [HttpPost("All")]
        [ProducesResponseType(typeof(List<BlogViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllBlogs(
            )
        {
            try
            {
                var result = await _blogService.GetAllBlogs();
                if (result == null || !result.Any())
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "List of Blogs Not Found!"
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
