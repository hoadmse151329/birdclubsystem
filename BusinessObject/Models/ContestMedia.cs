using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ContestMedia
    {
        public int PictureId { get; set; }
        public int? ContestId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual Contest? Contest { get; set; }
    }
}
