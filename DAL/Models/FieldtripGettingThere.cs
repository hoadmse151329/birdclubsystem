using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldtripGettingThere")]
    public partial class FieldtripGettingThere
    {
        public FieldtripGettingThere()
        {
            GettingThereStartEnd = "Some Description";
            GettingThereFlight = "Some Description";
            GettingThereTransportation = "Some Description";
            GettingThereAccommodation = "Some Description";
        }
        [Column("tripId")]
        public int TripId { get; set; }
        [Key]
        [Column("gettingThereId")]
        public int GettingThereId { get; set; }
        [Column("gettingThereStartEnd")]
        public string GettingThereStartEnd { get; set; } = null!;
        [Column("gettingThereFlight")]
        public string GettingThereFlight { get; set; } = null!;
        [Column("gettingThereTransportation")]
        public string GettingThereTransportation { get; set; } = null!;
        [Column("gettingThereAccommodation")]
        public string GettingThereAccommodation { get; set; } = null!;

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripGettingTheres))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
