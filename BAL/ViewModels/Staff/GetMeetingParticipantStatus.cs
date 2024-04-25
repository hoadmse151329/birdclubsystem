using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Staff
{
    public class GetMeetingParticipantStatus
    {
        public int MeetingId { get; set; }
        public string? MemberId { get; set; }
        public string? ParticipantNo { get; set; }
    }
}
