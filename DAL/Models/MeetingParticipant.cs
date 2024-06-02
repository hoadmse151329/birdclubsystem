using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("MeetingParticipant")]
    public partial class MeetingParticipant
    {
        public MeetingParticipant()
        {
            CheckInStatus = "Not Checked-In";
        }
        [Key]
        [Column("meetingId")]
        public int MeetingId { get; set; }
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Column("participantNo")]
        public int? ParticipantNo { get; set; }
        [Column("checkInStatus")]
        [StringLength(50)]
        public string? CheckInStatus { get; set; }

        [ForeignKey(nameof(MeetingId))]
        [InverseProperty(nameof(Meeting.MeetingParticipants))]
        public virtual Meeting MeetingDetails { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.MeetingParticipants))]
        public virtual Member MemberDetails { get; set; } = null!;
    }
}
