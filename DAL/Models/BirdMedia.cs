using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class BirdMedia
    {
        [Key]
        [Column("pictureId")]
        public int PictureId { get; set; }
        [Column("birdId")]
        public int? BirdId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("image")]
        public string? Image { get; set; }

        [ForeignKey(nameof(BirdId))]
        [InverseProperty(nameof(Bird.BirdPictures))]
        public virtual Bird? BirdDetails { get; set; }
    }
}
