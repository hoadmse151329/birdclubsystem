using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BAL.ViewModels
{
    public class ContestMediaViewModel
    {
        public ContestMediaViewModel()
        {
            PictureId = 0;
            Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/contest/contest_image_1.png";
        }
        public int? PictureId { get; set; }
        public int? ContestId { get; set; }
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
