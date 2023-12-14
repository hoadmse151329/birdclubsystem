using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class MeetingParticipant
    {
        public int? MeetingId { get; set; }
        public int? MemberId { get; set; }
        public string ParticipantNo { get; set; } = null!;

        public virtual Meeting? Meeting { get; set; }
        public virtual Member? Member { get; set; }
    }
}
