using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FieldTripParticipant
    {
        public int TripId { get; set; }
        public int MemberId { get; set; }
        public string ParticipantNo { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
