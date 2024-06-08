using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Galleries = new HashSet<Gallery>();
            NewsList = new HashSet<News>();
            Notifications = new HashSet<Notification>();
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
        [StringLength(255)]
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
        [InverseProperty(nameof(Member.UserDetails))]
        public virtual Member? MemberDetails { get; set; }
        [InverseProperty(nameof(Blog.UserDetails))]
        public virtual ICollection<Blog> Blogs { get; set; }
        [InverseProperty(nameof(Comment.UserDetails))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(Feedback.UserDetails))]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [InverseProperty(nameof(Gallery.UserDetails))]
        public virtual ICollection<Gallery> Galleries { get; set; }
        [InverseProperty(nameof(News.UserDetails))]
        public virtual ICollection<News> NewsList { get; set; }
        [InverseProperty(nameof(Notification.UserDetails))]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(Transaction.UserDetails))]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}