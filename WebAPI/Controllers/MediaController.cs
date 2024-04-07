using BAL.Services.Implements;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        public readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var response = await _mediaService.UploadFiles(files);
            return Ok(response);
        }

        [HttpGet("BlobList")]
        public async Task<IActionResult> GetBlobList()
        {
            var response = await _mediaService.GetUploadedBlob();
            return Ok(response);
        }
    }
}
