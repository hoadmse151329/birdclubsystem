using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class ContestParticipantViewModel
    {
        public int? ContestId { get; set; }
        public string? MemberId { get; set; }
        public string? MemberName { get; set; }
        public int? BirdId { get; set; }
        public int? Elo { get; set; }
        public string? ParticipantNo { get; set; }
        public string? CheckInStatus { get; set; }
    }
}
