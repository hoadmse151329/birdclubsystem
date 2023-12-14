using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FieldtripGettingThere
    {
        public int TripId { get; set; }
        public int GettingThereId { get; set; }
        public string GettingThereText { get; set; } = null!;

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
