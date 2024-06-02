using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class MeetingMedia
    {
        [Key]
        [Column("pictureId")]
        public int PictureId { get; set; }
        [Column("meetingId")]
        public int? MeetingId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }

        [ForeignKey(nameof(MeetingId))]
        [InverseProperty(nameof(Meeting.MeetingPictures))]
        public virtual Meeting? MeetingDetail { get; set; }
    }
}
