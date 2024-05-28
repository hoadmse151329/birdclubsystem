using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            Status = "Active";
            UploadDate = DateTime.Now;
        }
        public int? NewsId { get; set; }
        [Required(ErrorMessage = "News Title is required")]
        [DisplayName("News title")]
        public string? Title { get; set; }
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
        [DisplayName("News headlines picture")]
        public string? Picture { get; set; }
        public int? UserId { get; set; }
    }
}
