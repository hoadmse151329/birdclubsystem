using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripDaybyDayViewModel
    {
        public FieldtripDaybyDayViewModel()
        {
            Media = new List<FieldtripMediaViewModel>();
        }
        //public int? TripId { get; set; }
        public int? DaybyDayId { get; set; }
        public int? Day { get; set; }
        public string? Description { get; set; }
        public string? MainDestination { get; set; }
        public string? Accommodation { get; set; }
        public string? MealsAndDrinks { get; set; }
        public string? PictureId { get; set; }
        public List<FieldtripMediaViewModel> Media { get; set; }
    }
}
