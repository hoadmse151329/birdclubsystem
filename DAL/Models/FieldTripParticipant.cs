using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class FieldTripParticipant
    {
        [Key]
        [Column("tripId")]
        public int TripId { get; set; }
        [Key]
        [Column("memberId")]
        [StringLength(50)]
        public string MemberId { get; set; } = null!;
        [Column("participantNo")]
        [StringLength(50)]
        public string ParticipantNo { get; set; } = null!;
        [Column("checkInStatus")]
        [StringLength(50)]
        public string CheckInStatus { get; set; } = null!;

        [ForeignKey(nameof(MemberId))]
        [InverseProperty("FieldTripParticipants")]
        public virtual Member Member { get; set; } = null!;
        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldTripParticipants))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
