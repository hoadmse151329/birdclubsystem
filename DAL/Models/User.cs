using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            News = new HashSet<News>();
            Transactions = new HashSet<Transaction>();
        }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
        public int? ClubId { get; set; }
        public string? MemberId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? ImagePath { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
