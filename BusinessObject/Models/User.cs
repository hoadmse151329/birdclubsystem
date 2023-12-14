using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            News = new HashSet<News>();
        }

        public int UserId { get; set; }
        public int? ClubId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? MemberId { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
