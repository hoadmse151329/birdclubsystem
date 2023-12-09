using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class BirdMedia
    {
        public int PictureId { get; set; }
        public int? BirdId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual Bird? Bird { get; set; }
    }
}
