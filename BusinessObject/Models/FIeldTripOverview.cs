using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FIeldTripOverview
    {
        public int tripId { get; set; }
        public string title { get; set; } = null!;
        public string description { get; set; } = null!;
        public int duration { get; set; }
        public double price { get; set; }
        public string destination { get; set; } = null!;
        public DateTime registrationDeadline { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int? pictureId { get; set; }
        public string? userReview { get; set; }
    }
}
