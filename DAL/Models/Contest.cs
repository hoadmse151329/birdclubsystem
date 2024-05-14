using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Contest")]
    public partial class Contest
    {
        public Contest()
        {
            ContestPictures = new HashSet<ContestMedia>();
            ContestParticipants = new HashSet<ContestParticipant>();
        }

        [Key]
        [Column("contestId")]
        public int ContestId { get; set; }
        [Column("contestName")]
        [StringLength(255)]
        public string ContestName { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("openRegistration", TypeName = "datetime")]
        public DateTime? OpenRegistration { get; set; }
        [Column("registrationDeadline", TypeName = "datetime")]
        public DateTime? RegistrationDeadline { get; set; }
        [Column("locationId")]
        public int? LocationId { get; set; }
        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("endDate", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("reqMinELO")]
        public int? ReqMinELO { get; set; }
        [Column("reqMaxELO")]
        public int? ReqMaxELO { get; set; }
        [Column("afterELO")]
        public int? AfterELO { get; set; }
        [Column("fee")]
        public int? Fee { get; set; }
        [Column("prize")]
        public int? Prize { get; set; }
        [Column("host")]
        [StringLength(100)]
        public string? Host { get; set; }
        [Column("incharge")]
        [StringLength(100)]
        public string? Incharge { get; set; }
        [Column("note")]
        public string? Note { get; set; }
        [Column("review")]
        public string? Review { get; set; }
        [Column("numberOfParticipants")]
        public int? NumberOfParticipants { get; set; }
        [Column("clubId")]
        public int? ClubId { get; set; }
        [Column("numberOfParticipantsLimit")]
        public int? NumberOfParticipantsLimit { get; set; }

        [InverseProperty(nameof(ContestMedia.ContestDetail))]
        public virtual ICollection<ContestMedia> ContestPictures { get; set; }
        [InverseProperty(nameof(ContestParticipant.ContestDetail))]
        public virtual ICollection<ContestParticipant> ContestParticipants { get; set; }
    }
}
