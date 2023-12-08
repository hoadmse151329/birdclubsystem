using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public string? Category { get; set; }
        public DateTime UploadDate { get; set; }
        public int Vote { get; set; }
        public string? Image { get; set; }
        public string Status { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
