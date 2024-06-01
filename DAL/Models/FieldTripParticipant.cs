using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldTripParticipants")]
    public partial class FieldTripParticipant
    {
        [Key]
        [Column("tripId")]
        public int TripId { get; set; }
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Column("participantNo")]
        public int? ParticipantNo { get; set; }
        [Column("checkInStatus")]
        [StringLength(50)]
        public string? CheckInStatus { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.FieldTripParticipants))]
        public virtual Member MemberDetails { get; set; } = null!;
        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldTripParticipants))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
