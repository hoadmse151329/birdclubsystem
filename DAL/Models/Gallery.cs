using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Gallery
    {
        public int? PictureId { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public string Image { get; set; } = null!;
    }
}
