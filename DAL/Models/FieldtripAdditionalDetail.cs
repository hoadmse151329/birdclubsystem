using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class FieldtripAdditionalDetail
    {
        [Key]
        [Column("tripDetailsId")]
        public int TripDetailsId { get; set; }
        [Column("tripId")]
        public int TripId { get; set; }
        [Column("title")]
        [StringLength(50)]
        public string? Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripAdditionalDetails))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
