using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("News")]
    public partial class News
    {
        [Key]
        [Column("newsId")]
        public int NewsId { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string? Title { get; set; }
        [Column("category")]
        [StringLength(50)]
        public string? Category { get; set; }
        [Column("newsContent")]
        public string? NewsContent { get; set; }
        [Column("uploadDate", TypeName = "datetime")]
        public DateTime? UploadDate { get; set; }
        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }
        [Column("picture")]
        [Unicode(false)]
        public string? Picture { get; set; }
        [Column("userId")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.NewsList))]
        public virtual User? UserDetails { get; set; }
    }
}
