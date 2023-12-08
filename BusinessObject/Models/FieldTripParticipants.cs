using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldTripParticipants
    {
        public int tripId { get; set; }
        public int memberId { get; set; }
        public string participantNo { get; set; } = null!;
    }
}
