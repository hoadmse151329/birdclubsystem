using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FieldTripOverview
    {
        public int TripId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Destination { get; set; } = null!;
        public DateTime RegistrationDeadline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? PictureId { get; set; }
        public string? UserReview { get; set; }

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
