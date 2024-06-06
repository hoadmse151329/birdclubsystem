using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class BlogViewModel
    {
        public BlogViewModel()
        {
            Status = "Draft";
            Image = "https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png";
            Vote = 0;
            UploadDate = DateTime.Now;
            Comments = new List<CommentViewModel>();
            MemberAvatar = "https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png";
        }
        public int? BlogId { get; set; }
        public int? UserId { get; set; }
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
        public string? Image { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public string? MemberAvatar { get; set; }
        public List<CommentViewModel>? Comments { get; set; }
    }
}
