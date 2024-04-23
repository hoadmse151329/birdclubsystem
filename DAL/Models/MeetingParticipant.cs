using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class MeetingParticipant
    {
        [Key, Column(Order = 0)]
        public int? MeetingId { get; set; }
        [Key, Column(Order = 1)]
        public string MemberId { get; set; } = null!;
        public string ParticipantNo { get; set; } = null!;
        public string CheckInStatus { get; set; } = null!;

        public virtual Meeting Meeting { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
