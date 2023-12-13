using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripRateViewModel
    {
        public int? TripId { get; set; }
        public int? RateId { get; set; }
        public string? RateType { get; set; }
        public decimal? Price { get; set; }
    }
}
