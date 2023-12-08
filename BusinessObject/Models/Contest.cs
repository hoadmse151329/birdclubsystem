using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Contest
    {
        public int contestId { get; set; }
        public string contestName { get; set; } = null!;
        public string? description { get; set; }
        public DateTime? registrationDeadline { get; set; }
        public int? locationId { get; set; }
        public string? status { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? beforeScore { get; set; }
        public int? afterScore { get; set; }
        public double? fee { get; set; }
        public double? price { get; set; }
        public string? host { get; set; }
        public string? incharge { get; set; }
        public string? note { get; set; }
        public string? review { get; set; }
        public int? numberOfParticipants { get; set; }
        public int? clubId { get; set; }
    }
}
