using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FieldtripRate
    {
        public int TripId { get; set; }
        public int RateId { get; set; }
        public string RateType { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
