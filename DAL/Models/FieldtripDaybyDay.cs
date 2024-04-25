using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("FieldtripDaybyDay")]
    public partial class FieldtripDaybyDay
    {
        [Column("tripId")]
        public int TripId { get; set; }
        [Key]
        [Column("dayByDayId")]
        public int DayByDayId { get; set; }
        [Column("day")]
        public int Day { get; set; }
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("mainDestination")]
        [StringLength(100)]
        public string? MainDestination { get; set; }
        [Column("accommodation")]
        [StringLength(255)]
        public string? Accommodation { get; set; }
        [Column("mealsAndDrinks")]
        [StringLength(255)]
        public string? MealsAndDrinks { get; set; }

        [ForeignKey(nameof(TripId))]
        [InverseProperty(nameof(FieldTrip.FieldtripDaybyDays))]
        public virtual FieldTrip Trip { get; set; } = null!;
    }
}
