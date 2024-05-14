using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldTrip")]
    public partial class FieldTrip
    {
        public FieldTrip()
        {
            FieldTripParticipants = new HashSet<FieldTripParticipant>();
            FieldtripAdditionalDetails = new HashSet<FieldtripAdditionalDetail>();
            FieldtripDaybyDays = new HashSet<FieldtripDaybyDay>();
            FieldtripGettingTheres = new FieldtripGettingThere();
            FieldtripInclusions = new HashSet<FieldtripInclusion>();
            FieldtripPictures = new HashSet<FieldtripMedia>();
        }

        [Key]
        [Column("tripId")]
        public int TripId { get; set; }
        [Column("tripName")]
        [StringLength(255)]
        public string TripName { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("details")]
        public string Details { get; set; } = null!;
        [Column("openRegistration", TypeName = "datetime")]
        public DateTime? OpenRegistration { get; set; }
        [Column("registrationDeadline", TypeName = "datetime")]
        public DateTime? RegistrationDeadline { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("endDate", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("locationId")]
        public int? LocationId { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }
        [Column("numberOfParticipants")]
        public int? NumberOfParticipants { get; set; }
        [Column("numberOfParticipantsLimit")]
        public int? NumberOfParticipantsLimit { get; set; }
        [Column("fee")]
        public int? Fee { get; set; }
        [Column("host")]
        [StringLength(100)]
        public string? Host { get; set; }
        [Column("inCharge")]
        [StringLength(100)]
        public string? InCharge { get; set; }
        [Column("note")]
        public string? Note { get; set; }

        [InverseProperty(nameof(FieldTripParticipant.Trip))]
        public virtual ICollection<FieldTripParticipant> FieldTripParticipants { get; set; }
        [InverseProperty(nameof(FieldtripAdditionalDetail.Trip))]
        public virtual ICollection<FieldtripAdditionalDetail> FieldtripAdditionalDetails { get; set; }
        [InverseProperty(nameof(FieldtripDaybyDay.Trip))]
        public virtual ICollection<FieldtripDaybyDay> FieldtripDaybyDays { get; set; }
        [InverseProperty(nameof(FieldtripGettingThere.Trip))]
        public virtual FieldtripGettingThere FieldtripGettingTheres { get; set; }
        [InverseProperty(nameof(FieldtripInclusion.Trip))]
        public virtual ICollection<FieldtripInclusion> FieldtripInclusions { get; set; }
        [InverseProperty(nameof(FieldtripMedia.Trip))]
        public virtual ICollection<FieldtripMedia> FieldtripPictures { get; set; }
    }
}
