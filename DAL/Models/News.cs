using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string NewsContent { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Picture { get; set; }
        public string? Filepdf { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
