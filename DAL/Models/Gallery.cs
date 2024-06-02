using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Gallery")]
    public partial class Gallery
    {
        [Key]
        [Column("pictureId")]
        public int PictureId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("image")]
        [Unicode(false)]
        public string Image { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Galleries))]
        public virtual User UserDetail { get; set; } = null!;
    }
}
