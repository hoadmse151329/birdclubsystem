using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class ContestParticipants
    {
        public int? contestId { get; set; }
        public int? birdId { get; set; }
        public int ELO { get; set; }
        public string participantNo { get; set; } = null!;
        public string checkInStatus { get; set; } = null!;
    }
}
