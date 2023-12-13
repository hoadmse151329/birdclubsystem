using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class BlogViewModel
    {
        public int? BlogId { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime? UploadDate { get; set; }
        public int? Vote { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
    }
}
