using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripDaybyDayViewModel
    {
        public int? TripId { get; set; }
        public int? DaybyDayId { get; set; }
        public int? Day { get; set; }
        public string? Description { get; set; }
        public string? PictureId { get; set; }
    }
}
