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
        [DisplayName("Getting There Start and End")]
        public string? GettingThereStartEnd { get; set; }
        //[Required(ErrorMessage = "Flight description is required")]
        [DisplayName("Getting There Flight")]
        public string? GettingThereFlight { get; set; }
        //[Required(ErrorMessage = "Transportation description is required")]
        [DisplayName("Getting There Flight")]
        public string? GettingThereTransportation { get; set; }
        //[Required(ErrorMessage = "Accommodation description is required")]
        [DisplayName("Getting There Accommodation")]
        public string? GettingThereAccommodation { get; set; }
    }
}
