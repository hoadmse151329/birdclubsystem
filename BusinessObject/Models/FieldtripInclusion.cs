using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FieldtripInclusion
    {
        public int TripId { get; set; }
        public int InclusionId { get; set; }
        public string Title { get; set; } = null!;
        public string InclusionText { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
