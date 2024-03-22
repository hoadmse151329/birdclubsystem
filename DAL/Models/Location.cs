using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Location
    {
        public Location()
        {
            ClubLocations = new HashSet<ClubLocation>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<ClubLocation> ClubLocations { get; set; }
    }
}
