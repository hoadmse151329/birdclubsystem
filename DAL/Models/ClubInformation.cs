using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class ClubInformation
    {
        public int ClubId { get; set; }
        public int? ClubLocationId { get; set; }
        public string? Description { get; set; }

        public virtual ClubLocation? ClubLocation { get; set; }
    }
}
