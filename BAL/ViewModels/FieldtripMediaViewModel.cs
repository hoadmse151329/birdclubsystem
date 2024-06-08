using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAL.ViewModels
{
    public class FieldtripMediaViewModel
    {
        public FieldtripMediaViewModel()
        {
            PictureId = 0;
            Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_1.png";
        }
        public int? PictureId { get; set; }
        public int? TripId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Image")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [DisplayName("Type")]
        public string? Type { get; set; }
        public int? DayByDayId { get; set; }
        public IFormFile? ImageUpload { get; set; }
        public List<SelectListItem> DayNumberSelectableList { get; set; }
    }
}
