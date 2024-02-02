using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            MeetingMedia = new HashSet<MeetingMedia>();
        }

        public int MeetingId { get; set; }
        public string MeetingName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfParticipants { get; set; }
        public string? Host { get; set; }
        public string? Incharge { get; set; }
        public string? Note { get; set; }
        public int? LocationId { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<MeetingMedia> MeetingMedia { get; set; }
    }
}
