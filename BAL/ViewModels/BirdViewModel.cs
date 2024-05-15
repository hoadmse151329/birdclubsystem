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
    public class BirdViewModel
    {
        public BirdViewModel()
        {
            BirdId = 0;
            BirdName = "New Bird";
            Age = 5;
            AddDate = DateTime.Now;
            Status = "Active";
            Elo = 1500;
            BirdMainImage = null;
            ProfilePic = "https://edwinbirdclubstorage.blob.core.windows.net/images/bird/bulbul_placeholder.jpg";
        }
        public int? BirdId { get; set; }
        public string? MemberId { get; set; }
        [Required(ErrorMessage = "Bird Name is required")]
        [DisplayName("Bird Name")]
        public string? BirdName { get; set; }
        [Required(ErrorMessage = "ELO is required")]
        [DisplayName("ELO")]
        public int? Elo { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [DisplayName("Age")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Color is required")]
        [DisplayName("Color")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Date Added is required")]
        [DisplayName("Date Added")]
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }
        [DisplayName("Profile Picture")]
        public string? ProfilePic { get; set; }
        [DisplayName("Status")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "Origin is required")]
        [DisplayName("Origin")]
        public string? Origin { get; set; }
        [DisplayName("Main Image")]
        public IFormFile? BirdMainImage { get; set; }
    }
}
