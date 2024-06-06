using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("Location")]
    public partial class Location
    {
        public Location()
        {
            ClubLocations = new HashSet<ClubLocation>();
            Description = "Some Address Description";
        }

        [Key]
        [Column("locationId")]
        public int LocationId { get; set; }
        [Column("locationName")]
        [StringLength(255)]
        public string LocationName { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;

        [InverseProperty(nameof(ClubLocation.Location))]
        public virtual ICollection<ClubLocation> ClubLocations { get; set; }
    }
}
