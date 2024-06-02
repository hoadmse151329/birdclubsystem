using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        [Column("commentId")]
        public int CommentId { get; set; }
        [Column("blogId")]
        public int? BlogId { get; set; }
        [Column("vote")]
        public int? Vote { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("userId")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Comments))]
        public virtual User UserDetail { get; set; } = null!;
    }
}
