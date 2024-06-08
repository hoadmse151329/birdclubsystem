using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet("All")]
        [Authorize(Roles = "Manager")]
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
        [HttpGet("SearchForMemberOrGuest")]
        [Authorize(Roles = "Member,Guest")]
        [ProducesResponseType(typeof(List<BlogViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchBlogByAttributes(
            [FromQuery] string? description,
            [FromQuery] string? category,
            [FromQuery] DateTime? uploadDate,
            [FromQuery] int? vote,
            [FromQuery] List<string>? status,
            [FromQuery] string? orderBy
            )
        {
            try
            {
                bool isMemberOrGuest = true;
                var result = await _blogService.GetSortedBlogs(
                    description: description,
                    category: category,
                    uploadDate: uploadDate,
                    vote: vote,
                    statuses: status,
                    orderBy: orderBy,
                    isMemberOrGuest: isMemberOrGuest
                    );
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
        [HttpGet("Search")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(List<BlogViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchBlogByAttributesManager(
            [FromQuery] string? description,
            [FromQuery] string? category,
            [FromQuery] DateTime? uploadDate,
            [FromQuery] int? vote,
            [FromQuery] List<string>? status,
            [FromQuery] string? orderBy
            )
        {
            try
            {
                bool isMemberOrGuest = false;
                var result = await _blogService.GetSortedBlogs(
                    description: description,
                    category: category,
                    uploadDate: uploadDate,
                    vote: vote,
                    statuses: status,
                    orderBy: orderBy,
                    isMemberOrGuest: isMemberOrGuest
                    );
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
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Manager,Member,Guest")]
        [ProducesResponseType(typeof(BlogViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBlogById(
            [FromRoute] int id)
        {
            try
            {
                var result = await _blogService.GetBlogByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Blog Not Found!"
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
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [Required][FromBody] CreateNewBlog blogVM)
        {
            try
            {

                _blogService.Create(blogVM);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Blog Create successfully !",
                    Data = blogVM
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
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [Required][FromRoute] int id,
            [FromBody] BlogViewModel blogVM)
        {
            try
            {
                var result = await _blogService.GetBlogByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Blog does not exist!"
                    });
                }
                blogVM.BlogId = id;
                _blogService.Update(blogVM);
                result = await _blogService.GetBlogByIdNoTracking(blogVM.BlogId.Value);
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
        [ProducesResponseType(typeof(BlogViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DisableBlog(
            [Required][FromRoute] int id)
        {
            try
            {
                var result = await _blogService.GetBlogByIdNoTracking(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Blog post does not exist!"
                    });
                }
                result.BlogId = id;
                result.Status = "Disabled";
                _blogService.Update(result);
                result = await _blogService.GetBlogByIdNoTracking(id);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Successfully Disabled Blog post!",
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
