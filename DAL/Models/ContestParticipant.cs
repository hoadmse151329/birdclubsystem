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
        [Key]
        [Column("contestId")]
        public int ContestId { get; set; }
        [Key]
        [Column("memberId")]
        [StringLength(255)]
        public string MemberId { get; set; } = null!;
        [Column("birdId")]
        public int? BirdId { get; set; }
        [Column("ELO")]
        public int? Elo { get; set; }
        [Column("score")]
        public int? Score { get; set; }
        [Column("participantNo")]
        public int? ParticipantNo { get; set; }
        [Column("checkInStatus")]
        [StringLength(50)]
        public string? CheckInStatus { get; set; }

        [ForeignKey(nameof(BirdId))]
        [InverseProperty(nameof(Bird.ContestParticipants))]
        public virtual Bird? BirdDetails { get; set; }
        [ForeignKey(nameof(ContestId))]
        [InverseProperty(nameof(Contest.ContestParticipants))]
        public virtual Contest ContestDetails { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(Member.ContestParticipants))]
        public virtual Member MemberDetails { get; set; } = null!;
    }
}