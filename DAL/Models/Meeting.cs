using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Meeting")]
    public partial class Meeting
    {
        public Meeting()
        {
            MeetingPictures = new HashSet<MeetingMedia>();
            MeetingParticipants = new HashSet<MeetingParticipant>();
        }

        [Key]
        [Column("meetingId")]
        public int MeetingId { get; set; }
        [Column("meetingName")]
        [StringLength(255)]
        public string MeetingName { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("openRegistration", TypeName = "datetime")]
        public DateTime? OpenRegistration { get; set; }
        [Column("registrationDeadline", TypeName = "datetime")]
        public DateTime? RegistrationDeadline { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("endDate", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("numberOfParticipants")]
        public int? NumberOfParticipants { get; set; }
        [Column("host")]
        [StringLength(100)]
        public string? Host { get; set; }
        [Column("incharge")]
        [StringLength(100)]
        public string? Incharge { get; set; }
        [Column("locationId")]
        public int? LocationId { get; set; }
        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }
        [Column("note")]
        public string? Note { get; set; }
        [Column("numberOfParticipantsLimit")]
        public int? NumberOfParticipantsLimit { get; set; }

        [InverseProperty(nameof(MeetingMedia.MeetingDetail))]
        public virtual ICollection<MeetingMedia> MeetingPictures { get; set; }
        [InverseProperty(nameof(MeetingParticipant.MeetingDetail))]
        public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
