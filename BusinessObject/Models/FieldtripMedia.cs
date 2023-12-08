using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FieldtripMedia
    {
        public int PictureId { get; set; }
        public int? TripId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual FieldTrip? Trip { get; set; }
    }
}
