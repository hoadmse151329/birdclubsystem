using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripGettingThereViewModel
    {
        public int? TripId { get; set; }
        public int? GettingThereId { get; set; }
        //[Required(ErrorMessage = "Start and End description is required")]
        [DisplayName("Start and End Details")]
        public string? GettingThereStartEnd { get; set; }
        //[Required(ErrorMessage = "Flight description is required")]
        [DisplayName("Flight Details")]
        public string? GettingThereFlight { get; set; }
        //[Required(ErrorMessage = "Transportation description is required")]
        [DisplayName("Transportation Details")]
        public string? GettingThereTransportation { get; set; }
        //[Required(ErrorMessage = "Accommodation description is required")]
        [DisplayName("Accommodation Details")]
        public string? GettingThereAccommodation { get; set; }
    }
}
