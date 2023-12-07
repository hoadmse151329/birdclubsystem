using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldTrip
    {
        public int tripId { get; set; }
        public string tripName { get; set; } = null!;
        public string? description { get; set; }
        public string details { get; set; } = null!;
        public DateTime? registrationDeadline { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? locationId { get; set; }
        public string? status { get; set; }
        public double? fee { get; set; }
        public int? numberOfParticipants { get; set; }
        public string? host { get; set; }
        public string? inCharge { get; set; }
        public string? note { get; set; }
        public string? review { get; set; }
    }
}
