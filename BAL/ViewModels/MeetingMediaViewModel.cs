using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingMediaViewModel
    {
        public MeetingMediaViewModel()
        {
            Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png";
            Description = "Image Description";
            Type = "Additional";
        }
        public int? PictureId { get; set; }
        public int? MeetingId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Image")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [DisplayName("Type")]
        public string? Type { get; set; }
        public IFormFile? ImageUpload { get; set; }
    }
}
