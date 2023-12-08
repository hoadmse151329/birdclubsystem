using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Meeting
    {
        public int meetingId { get; set; }
        public string meetingName { get; set; } = null!;
        public string? description { get; set; }
        public DateTime? registrationDeadline { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? numberOfParticipants { get; set; }
        public string? host { get; set; }
        public string? incharge { get; set; }
        public string? note { get; set; }
    }
}
