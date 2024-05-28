using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAL.ViewModels.News
{
    public class CreateNewNews
    {
        public CreateNewNews()
        {
            Status = "Active";
            UploadDate = DateTime.Now;
            Picture = "https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png";
            DefaultNewsCategorySelectList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Category", Value = ""},
                new SelectListItem { Text = "Announcement", Value = "Announcement" },
                new SelectListItem { Text = "Meeting", Value = "Meeting" },
                new SelectListItem { Text = "Fieldtrip", Value = "Fieldtrip" },
                new SelectListItem { Text = "Contest", Value = "Contest" },
                new SelectListItem { Text = "Others", Value = "Others" },
            };
        }
        public int? NewsId { get; set; }
        [Required(ErrorMessage = "News Title is required")]
        [DisplayName("News title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please select a role")]
        [DisplayName("Category")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "News Content is required")]
        [DisplayName("Content")]
        public string? NewsContent { get; set; }
        [Required(ErrorMessage = "Upload date is required")]
        [DisplayName("News upload date")]
        public DateTime? UploadDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        [DisplayName("News main Image")]
        public string? Picture { get; set; }
        public IFormFile? ImageUpload { get; set; }
        [DisplayName("User Id")]
        public string? MemberId { get; set; }
        public List<SelectListItem> DefaultNewsCategorySelectList { get; set; }
    }
}
