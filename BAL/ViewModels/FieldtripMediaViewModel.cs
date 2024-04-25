using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripMediaViewModel
    {
        public int? PictureId { get; set; }
        public int? TripId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public int? DayByDayId { get; set; }
    }
}
