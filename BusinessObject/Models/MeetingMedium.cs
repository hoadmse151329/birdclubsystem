using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class MeetingMedium
    {
        public int PictureId { get; set; }
        public int? MeetingId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual Meeting? Meeting { get; set; }
    }
}
