using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Galleries = new HashSet<Gallery>();
            NewsDetail = new HashSet<News>();
            Notifications = new HashSet<Notification>();
            Transactions = new HashSet<Transaction>();
            MemberDetail = new Member();
        }

        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("clubId")]
        public int? ClubId { get; set; }
        [Column("imagePath")]
        [StringLength(255)]
        public string? ImagePath { get; set; }
        [Column("memberId")]
        [StringLength(50)]
        public string? MemberId { get; set; }
        [Column("userName")]
        [StringLength(255)]
        public string? UserName { get; set; }
        [Column("password")]
        [StringLength(255)]
        public string? Password { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string? Role { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.UserDetail))]
        public virtual Member? MemberDetail { get; set; }
        [InverseProperty(nameof(Blog.UserDetail))]
        public virtual ICollection<Blog> Blogs { get; set; }
        [InverseProperty(nameof(Comment.UserDetail))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(Feedback.UserDetail))]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [InverseProperty(nameof(Gallery.UserDetail))]
        public virtual ICollection<Gallery> Galleries { get; set; }
        [InverseProperty(nameof(News.UserDetail))]
        public virtual ICollection<News> NewsDetail { get; set; }
        [InverseProperty(nameof(Notification.UserDetail))]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(Transaction.UserDetail))]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
