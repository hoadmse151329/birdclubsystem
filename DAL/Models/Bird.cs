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
            BirdPictures = new HashSet<BirdMedia>();
            ContestParticipants = new HashSet<ContestParticipant>();
        }

        [Key]
        [Column("birdId")]
        public int BirdId { get; set; }
        [Column("memberId")]
        [StringLength(255)]
        public string? MemberId { get; set; }
        [Column("birdName")]
        public string? BirdName { get; set; }
        [Column("ELO")]
        public int? Elo { get; set; }
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
        [InverseProperty(nameof(Member.Birds))]
        public virtual Member? MemberDetails { get; set; }
        [InverseProperty(nameof(BirdMedia.BirdDetails))]
        public virtual ICollection<BirdMedia> BirdPictures { get; set; }
        [InverseProperty(nameof(ContestParticipant.BirdDetails))]
        public virtual ICollection<ContestParticipant> ContestParticipants { get; set; }
    }
}
