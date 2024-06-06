using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace BAL.ViewModels.Blog
{
    public class CreateNewBlog
    {
        public CreateNewBlog()
        {
            Status = "Draft";
            Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png";
            Vote = 0;
            UploadDate = DateTime.Now;
            DefaultBlogCategorySelectList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Category", Value = ""},
                new SelectListItem { Text = "Discussion", Value = "Discussion" },
                new SelectListItem { Text = "Announcement", Value = "Announcement" },
                new SelectListItem { Text = "Information", Value = "Information" },
                new SelectListItem { Text = "Others", Value = "Others" },
            };
            MemberAvatar = "https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png";
        }
        public int? BlogId { get; set; }
        [Required(ErrorMessage = "Blog Author Name is required")]
        [DisplayName("Author Name")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Blog Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Blog Category is required")]
        [DisplayName("Blog Category")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "Blog upload date is required")]
        [DisplayName("Upload Date")]
        public DateTime? UploadDate { get; set; }
        public int? Vote { get; set; }
        public IFormFile? ImageUpload { get; set; }
        [DisplayName("Post Main Image")]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public string? MemberId { get; set; }
        [DisplayName("User Avatar")]
        public string? MemberAvatar { get; set; }
        public List<SelectListItem> DefaultBlogCategorySelectList { get; set; }
    }
}
