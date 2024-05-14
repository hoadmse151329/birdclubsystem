using BAL.Services.Interfaces;
using BAL.ViewModels;
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
        [HttpPost("{birdId:int}")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBirdById([FromRoute][Required] int birdId)
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
        [HttpPost("{id}/Create/Bird")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBird(
            [Required][FromRoute] string id,
            [Required][FromBody] BirdViewModel bird)
        {
            try
            {
                if (await _birdService.Create(id, bird))
                    return Ok(new
                    {
                        Status = true,
                        Message = "Bird Create successfully!",
                        Data = true
                    });
                else return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = true,
                    Message = "Bird Create Failed!",
                    Data = false
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
        [HttpPut("{memberId}/Bird/{birdId:int}/Update")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(BirdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBird(
            [Required][FromRoute] string memberId,
            [Required][FromRoute] int birdId,
            [Required][FromBody] BirdViewModel birdModel)
        {
            try
            {
                var check = _memberService.GetById(memberId).Result;
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
                var result = await _birdService.Update(memberId, birdModel);
                if (result) return Ok(new
                {
                    Status = true,
                    Data = result
                });
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
