using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Models; // Replace with the actual namespace of your models
using DAL.Repositories;
using DAL.Repositories.Implements;
using static DAL.Repositories.Implements.UserRepository;

[ApiController]
[Route("api/[controller]")]
public class ChangeImageController : ControllerBase
{
    private const string ERROR = "~/Home/Error";
    private const string SUCCESS = "~/Manager/ManagerProfile";

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadImage(IFormFile photo, string userID)
    {
        try
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(pathImage, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var image = "images/" + fileName;

                // Replace "UserManager" with the actual class managing users
                var userManager = new UserManager(); // Replace with the actual class managing users
                var check = userManager.ChangeImage(userID, image);

                if (check != null)
                {
                    return Ok(new { Message = "Image uploaded successfully", RedirectUrl = SUCCESS });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }

        return BadRequest(new { Message = "Image upload failed", RedirectUrl = ERROR });
    }
}
