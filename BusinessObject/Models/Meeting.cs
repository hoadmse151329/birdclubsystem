using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            MeetingMedia = new HashSet<MeetingMedium>();
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

        public virtual ICollection<MeetingMedium> MeetingMedia { get; set; }
    }
}
