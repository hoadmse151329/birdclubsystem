using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldtripInclusion")]
    public partial class FieldtripInclusion
    {
        [Column("tripId")]
        public int? TripId { get; set; }
        [Key]
        [Column("inclusionId")]
        public int InclusionId { get; set; }
        [Column("title")]
        [StringLength(100)]
        public string? Title { get; set; }
        [Column("inclusionText")]
        public string? InclusionText { get; set; }
        [Column("type")]
        [StringLength(50)]
        public string? Type { get; set; }
        [Column("inclusiontype")]
        [StringLength(50)]
        public string? Inclusiontype { get; set; }

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldTripInclusions))]
        public virtual FieldTrip? Trip { get; set; }
    }
}
