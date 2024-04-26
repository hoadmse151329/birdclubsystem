using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        [Column("blogId")]
        public int BlogId { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("category")]
        [StringLength(255)]
        public string? Category { get; set; }
        [Column("uploadDate", TypeName = "datetime")]
        public DateTime UploadDate { get; set; }
        [Column("vote")]
        public int Vote { get; set; }
        [Column("image")]
        [Unicode(false)]
        public string? Image { get; set; }
        [Column("status")]
        [StringLength(20)]
        public string Status { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Blogs))]
        public virtual User UserDetail { get; set; } = null!;
    }
}
