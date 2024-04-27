using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class FieldtripMedia
    {
        [Key]
        [Column("pictureId")]
        public int PictureId { get; set; }
        [Column("tripId")]
        public int? TripId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }
        [Column("dayByDayId")]
        public int? DayByDayId { get; set; }

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripPictures))]
        public virtual FieldTrip? Trip { get; set; }
    }
}
