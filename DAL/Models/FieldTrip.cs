using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FieldTrip
    {
        public FieldTrip()
        {
            FieldtripMedia = new HashSet<FieldtripMedia>();
        }

        public int TripId { get; set; }
        public string TripName { get; set; } = null!;
        public string? Description { get; set; }
        public string Details { get; set; } = null!;
        public DateTime? RegistrationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LocationId { get; set; }
        public string? Status { get; set; }
        public decimal? Fee { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfParticipantsLimit { get; set; }
        public string? Host { get; set; }
        public string? InCharge { get; set; }
        public string? Note { get; set; }
        public string? Review { get; set; }

        public virtual ICollection<FieldtripMedia> FieldtripMedia { get; set; }
    }
}
