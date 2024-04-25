using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ClubInformation")]
    public partial class ClubInformation
    {
        [Key]
        [Column("clubId")]
        public int ClubId { get; set; }
        [Column("clubLocationId")]
        public int? ClubLocationId { get; set; }
        [Column("description")]
        public string? Description { get; set; }

        [ForeignKey(nameof(ClubLocationId))]
        [InverseProperty("ClubInformations")]
        public virtual ClubLocation? ClubLocation { get; set; }
    }
}
