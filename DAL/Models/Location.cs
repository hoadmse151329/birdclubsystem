using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
