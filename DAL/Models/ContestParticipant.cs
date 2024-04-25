using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class ContestParticipant
    {
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
        [Column("participantNo")]
        [StringLength(50)]
        public string ParticipantNo { get; set; } = null!;
        [Column("checkInStatus")]
        [StringLength(50)]
        public string CheckInStatus { get; set; } = null!;

        [ForeignKey(nameof(BirdId))]
        [InverseProperty("ContestParticipants")]
        public virtual Bird? Bird { get; set; }
        [ForeignKey(nameof(ContestId))]
        [InverseProperty("ContestParticipants")]
        public virtual Contest Contest { get; set; } = null!;
        [ForeignKey(nameof(MemberId))]
        [InverseProperty("ContestParticipants")]
        public virtual Member Member { get; set; } = null!;
    }
}
