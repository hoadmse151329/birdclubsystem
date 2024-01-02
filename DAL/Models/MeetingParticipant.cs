using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{

    public partial class MeetingParticipant
    {
        [Key, Column(Order = 0)]
        public int? MeetingId { get; set; }
        [Key, Column(Order = 1)]
        public int? MemberId { get; set; }
        public string ParticipantNo { get; set; } = null!;

        public virtual Meeting? Meeting { get; set; }
        public virtual Member? Member { get; set; }
    }
}
