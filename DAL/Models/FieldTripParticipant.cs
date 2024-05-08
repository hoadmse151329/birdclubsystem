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
        public FieldTripParticipant()
        {
            CheckInStatus = "Not Checked-In";
        }
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
        [InverseProperty(nameof(FieldTrip.FieldTripParticipants))]
        public virtual Member MemberDetail { get; set; } = null!;
        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldTripParticipants))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
