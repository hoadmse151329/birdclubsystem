using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private readonly IBirdService _birdService;
        private readonly IMemberService _memberService;
        public BirdController(IBirdService birdService, IMemberService memberService)
        {
            _birdService = birdService;
            _memberService = memberService;
        }
        [HttpPost("AllBirds")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<BirdViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBirdsByMemberId([FromBody] string memberId)
        {
            try
            {
                var mem = await _memberService.GetBoolById(memberId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"

                });
                var result = await _birdService.GetBirdsByMemberId(memberId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Birds Found!"
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
        [HttpGet("{birdId:int}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBirdById(
            [FromRoute][Required] int birdId
            )
        {
            try
            {
                var bird = await _birdService.GetById(birdId);
                if (bird == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Bird Not Found!"

                });
                return Ok(new
                {
                    Status = true,
                    Data = bird
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
        [HttpPost("Create")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBird(
            [Required][FromBody] BirdViewModel bird)
        {
            try
            {
                var birdExistedName = await _birdService.GetByBirdName(bird.BirdName);
                if(birdExistedName != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = true,
                        Message = "Bird create failed, bird name already existed",
                        BoolData = false
                    });
                }
                if (await _birdService.Create(bird.MemberId, bird))
                {
                    var birdResult = await _birdService.GetByBirdName(bird.BirdName);
                    return Ok(new
                    {
                        Status = true,
                        Message = "Bird Create successfully!",
                        Data = birdResult
                    });
                }
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = false,
                    Message = "Bird Create Failed!",
                    BoolData = false
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
        [HttpPut("{birdId:int}/Update")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBird(
            [Required][FromRoute] int birdId,
            [Required][FromBody] BirdViewModel birdModel)
        {
            try
            {
                var check = _memberService.GetById(birdModel.MemberId).Result;
                if (check == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member does not exist!"
                });
                var bird = await _birdService.GetById(birdId);
                if (bird == null) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Bird does not exist!"
                });
                var result = await _birdService.Update(birdModel.MemberId, birdModel);
                if (result)
                {
                    bird = await _birdService.GetById(birdId);
                    return Ok(new
                    {
                        Status = true,
                        Data = bird
                    });
                }
                return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member does not exist or internal server error"
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
