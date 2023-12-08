using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ContestParticipant
    {
        public int? ContestId { get; set; }
        public int? BirdId { get; set; }
        public int Elo { get; set; }
        public string ParticipantNo { get; set; } = null!;
        public string CheckInStatus { get; set; } = null!;

        public virtual Bird? Bird { get; set; }
        public virtual Contest? Contest { get; set; }
    }
}
