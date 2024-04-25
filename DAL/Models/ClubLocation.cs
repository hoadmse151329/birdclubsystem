using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ClubLocation")]
    public partial class ClubLocation
    {
        public ClubLocation()
        {
            ClubInformations = new HashSet<ClubInformation>();
        }

        [Key]
        [Column("clubLocationId")]
        public int ClubLocationId { get; set; }
        [Column("clubName")]
        [StringLength(255)]
        public string? ClubName { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("locationId")]
        public int? LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("ClubLocations")]
        public virtual Location? Location { get; set; }
        [InverseProperty(nameof(ClubInformation.ClubLocation))]
        public virtual ICollection<ClubInformation> ClubInformations { get; set; }
    }
}
