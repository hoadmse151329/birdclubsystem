using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class FieldtripInclusion
    {
        [Column("tripId")]
        public int TripId { get; set; }
        [Key]
        [Column("inclusionId")]
        public int InclusionId { get; set; }
        [Column("title")]
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [Column("inclusionText")]
        public string InclusionText { get; set; } = null!;
        [Column("type")]
        [StringLength(50)]
        public string Type { get; set; } = null!;
        [Column("inclusiontype")]
        [StringLength(50)]
        public string Inclusiontype { get; set; } = null!;

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripInclusions))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
