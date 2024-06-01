using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldTripGettingThere")]
    public partial class FieldtripGettingThere
    {
        [Column("tripId")]
        public int? TripId { get; set; }
        [Key]
        [Column("gettingThereId")]
        public int GettingThereId { get; set; }
        [Column("gettingThereStartEnd")]
        public string? GettingThereStartEnd { get; set; }
        [Column("gettingThereFlight")]
        public string? GettingThereFlight { get; set; }
        [Column("gettingThereTransportation")]
        public string? GettingThereTransportation { get; set; }
        [Column("gettingThereAccommodation")]
        public string? GettingThereAccommodation { get; set; }

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldTripGettingThereDetails))]
        public virtual FieldTrip? Trip { get; set; }
    }
}
