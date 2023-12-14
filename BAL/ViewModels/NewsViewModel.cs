using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class NewsViewModel
    {
        public int? NewsId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? NewsContent { get; set; }
        public DateTime? UploadDate { get; set; }
        public string? Status { get; set; }
        public string? Picture { get; set; }
        public string? Filepdf { get; set; }
        public int? UserId { get; set; }
    }
}
