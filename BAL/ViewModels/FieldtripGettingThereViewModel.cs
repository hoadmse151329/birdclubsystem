using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripGettingThereViewModel
    {
        public int? TripId { get; set; }
        public int? GettingThereId { get; set; }
        public string? GettingThereStartEnd { get; set; }
        public string? GettingThereFlight { get; set; }
        public string? GettingThereTransportation { get; set; }
        public string? GettingThereAccommodation { get; set; }
    }
}
