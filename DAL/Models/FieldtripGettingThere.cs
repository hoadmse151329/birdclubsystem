using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FieldtripGettingThere
    {
        public int TripId { get; set; }
        public int GettingThereId { get; set; }
        public string GettingThereText { get; set; } = null!;

        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
