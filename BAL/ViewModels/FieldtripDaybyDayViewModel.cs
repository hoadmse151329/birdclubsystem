using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public int? TripId { get; set; }
        public int? DaybyDayId { get; set; }
        [Required(ErrorMessage = "Day Number value is required")]
        [Range(1,99, ErrorMessage = "Day Number value must not be less than {0} or more than {1}")]
        [DisplayName("Trip Day")]
        public int? Day { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Main Destination is required")]
        [DisplayName("Trip Main Destination")]
        [StringLength(100,ErrorMessage = "Main Destination name length must not be longer than {0} characters")]
        [MaxLength(100, ErrorMessage = "Main Destination name length must not be longer than {0} characters")]
        public string? MainDestination { get; set; }
        [Required(ErrorMessage = "Accommodation name is required")]
        [DisplayName("Trip Accommodation")]
        [StringLength(255, ErrorMessage = "Accommodation name length must not be longer than {0} characters")]
        [MaxLength(255, ErrorMessage = "Accommodation name length must not be longer than {0} characters")]
        public string? Accommodation { get; set; }
        [Required(ErrorMessage = "Meals and Drinks description is required")]
        [DisplayName("Additional Meals and Drinks")]
        [StringLength(255, ErrorMessage = "Meals And Drinks description length must not be longer than {0} characters")]
        [MaxLength(255, ErrorMessage = "Meals And Drinks description length must not be longer than {0} characters")]
        public string? MealsAndDrinks { get; set; }
        public List<FieldtripMediaViewModel> Media { get; set; }
    }
}
