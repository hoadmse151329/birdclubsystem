using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingParticipantViewModel
    {
        public int? MeetingId { get; set; }
        public int? MemberId { get; set; }
        public string? ParticipantNo { get; set; }
    }
}
