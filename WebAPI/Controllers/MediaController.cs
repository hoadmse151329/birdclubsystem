using Azure.Storage.Blobs;
using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        public readonly IMediaService _mediaService;
        private readonly IConfiguration _config;

        private string GenerateFileName(string fileName, string userId)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = userId + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/"
                   + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }

        public MediaController(IMediaService mediaService, IConfiguration config)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var response = await _mediaService.UploadFiles(files);
            return Ok(response);
        }

        /*[HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UpdateMemberAvatar imagePath)
        {
            try
            {
                var filename = GenerateFileName(imagePath.ImagePath, imagePath.MemberId);
                var fileUrl = "";
                BlobContainerClient container = 
            }
        }*/

        [HttpGet("BlobList")]
        public async Task<IActionResult> GetBlobList()
        {
            var response = await _mediaService.GetUploadedBlob();
            return Ok(response);
        }
    }
}
