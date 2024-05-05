using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class ContestMedia
    {
        [Key]
        [Column("pictureId")]
        public int PictureId { get; set; }
        [Column("contestId")]
        public int? ContestId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }

        [ForeignKey(nameof(ContestId))]
        [InverseProperty(nameof(Contest.ContestPictures))]
        public virtual Contest? ContestDetail { get; set; }
    }
}
