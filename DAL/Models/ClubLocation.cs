using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class ClubLocation
    {
        public ClubLocation()
        {
            ClubInformations = new HashSet<ClubInformation>();
        }

        public int ClubLocationId { get; set; }
        public string? ClubName { get; set; }
        public string? Description { get; set; }
        public int? LocationId { get; set; }

        public virtual Location? Location { get; set; }
        public virtual ICollection<ClubInformation> ClubInformations { get; set; }
    }
}
