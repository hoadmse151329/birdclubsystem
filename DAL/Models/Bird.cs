using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Bird")]
    public partial class Bird
    {
        public Bird()
        {
            BirdMedia = new HashSet<BirdMedia>();
            ContestParticipants = new HashSet<ContestParticipant>();
            ContestScores = new HashSet<ContestScore>();
        }

        [Key]
        [Column("birdId")]
        public int BirdId { get; set; }
        [Column("memberId")]
        [StringLength(50)]
        public string MemberId { get; set; } = null!;
        [Column("birdName")]
        public string BirdName { get; set; } = null!;
        [Column("ELO")]
        public int Elo { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("color")]
        public string? Color { get; set; }
        [Column("addDate", TypeName = "datetime")]
        public DateTime? AddDate { get; set; }
        [Column("profilePic")]
        public string? ProfilePic { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }
        [Column("origin")]
        public string? Origin { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty("Birds")]
        public virtual Member Member { get; set; } = null!;
        [InverseProperty(nameof(Models.BirdMedia.Bird))]
        public virtual ICollection<BirdMedia> BirdMedia { get; set; }
        [InverseProperty(nameof(ContestParticipant.Bird))]
        public virtual ICollection<ContestParticipant> ContestParticipants { get; set; }
        [InverseProperty(nameof(ContestScore.Bird))]
        public virtual ICollection<ContestScore> ContestScores { get; set; }
    }
}
