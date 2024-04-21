using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class ContestParticipant
    {
        public int ContestId { get; set; }
		public string MemberId { get; set; }
		public int? BirdId { get; set; }
        public int Elo { get; set; }
        public string ParticipantNo { get; set; } = null!;
        public int CheckInStatus { get; set; }

        public virtual Bird? Bird { get; set; }
		public virtual Member? Member { get; set; }
		public virtual Contest? Contest { get; set; }
    }
}
