using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? BlogId { get; set; }
        public int? Vote { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
