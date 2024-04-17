using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FieldTripParticipant
    {
        public int TripId { get; set; }
        public string MemberId { get; set; } = null!;
        public string ParticipantNo { get; set; } = null!;
        public int CheckInStatus { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
