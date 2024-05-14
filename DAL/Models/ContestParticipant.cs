using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ContestParticipants")]
    public partial class ContestParticipant
    {
        public ContestParticipant()
        {
            CheckInStatus = "Not Checked-In";
            Score = 0;
        }
        [Key]
        [Column("contestId")]
        public int ContestId { get; set; }
        [Key]
        [Column("memberId")]
        [StringLength(50)]
        public string MemberId { get; set; } = null!;
        [Column("birdId")]
        public int? BirdId { get; set; }
        [Column("ELO")]
        public int Elo { get; set; }
        [Column("score")]
        public int Score { get; set; }
        [Column("participantNo")]
        [StringLength(50)]
        public string ParticipantNo { get; set; } = null!;
        [Column("checkInStatus")]
        [StringLength(50)]
        public string CheckInStatus { get; set; } = null!;

        [ForeignKey(nameof(BirdId))]
        [InverseProperty(nameof(Contest.ContestParticipants))]
        public virtual Bird? BirdDetails { get; set; }
        [ForeignKey(nameof(ContestId))]
        [InverseProperty(nameof(Contest.ContestParticipants))]
        public virtual Contest ContestDetail { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Contest.ContestParticipants))]
        public virtual Member MemberDetail { get; set; } = null!;
    }
}
