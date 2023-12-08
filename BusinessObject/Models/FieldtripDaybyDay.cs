using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FieldtripDaybyDay
    {
        public int TripId { get; set; }
        public int DaybyDayId { get; set; }
        public int Day { get; set; }
        public string Description { get; set; } = null!;
        public string? PictureId { get; set; }

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
