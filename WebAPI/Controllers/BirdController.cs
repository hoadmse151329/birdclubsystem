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
        public BirdController(IBirdService birdService)
        {
            _birdService = birdService;
        }
        /*[HttpPost("Bird")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<BirdViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBirds([Required][FromBody] string memberId)
        {
            try
            {

            }
    }*/
    }
}
