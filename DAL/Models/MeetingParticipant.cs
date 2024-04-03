using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class MeetingParticipant
    {
        public int? MeetingId { get; set; }
        public string? MemberId { get; set; }
        public string ParticipantNo { get; set; } = null!;

        public virtual Meeting? Meeting { get; set; }
        public virtual Member? Member { get; set; }
    }
}
