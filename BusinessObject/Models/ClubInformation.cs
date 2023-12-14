using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ClubInformation
    {
        public int ClubId { get; set; }
        public int? ClubLocationId { get; set; }
        public string? Description { get; set; }
    }
}
