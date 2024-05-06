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
        [StringLength(50)]
        public string MemberId { get; set; } = null!;
        [Column("participantNo")]
        [StringLength(50)]
        public string ParticipantNo { get; set; } = null!;
        [Column("checkInStatus")]
        [StringLength(50)]
        public string CheckInStatus { get; set; } = null!;

        [ForeignKey(nameof(MeetingId))]
        [InverseProperty(nameof(Meeting.MeetingParticipants))]
        public virtual Meeting MeetingDetail { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Meeting.MeetingParticipants))]
        public virtual Member MemberDetail { get; set; } = null!;
    }
}
