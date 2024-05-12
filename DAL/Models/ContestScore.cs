using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ContestScore")]
    public partial class ContestScore
    {
        [Key]
        [Column("scoreId")]
        public int ScoreId { get; set; }
        [Column("contestId")]
        public int? ContestId { get; set; }
        [Column("birdId")]
        public int? BirdId { get; set; }
        [Column("memberId")]
        [StringLength(50)]
        public string? MemberId { get; set; }
        [Column("score", TypeName = "decimal(5, 2)")]
        public decimal? Score { get; set; }
        [Column("scoreDate", TypeName = "datetime")]
        public DateTime? ScoreDate { get; set; }
        [Column("comment")]
        public string? Comment { get; set; }

        [ForeignKey(nameof(BirdId))]
        [InverseProperty(nameof(Bird.ContestScores))]
        public virtual Bird? BirdDetails { get; set; }
    }
}
