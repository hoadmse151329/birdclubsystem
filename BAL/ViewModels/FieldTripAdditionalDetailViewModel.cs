using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldTripAdditionalDetailViewModel
    {
        public FieldTripAdditionalDetailViewModel()
        {
            Type = "tour_features";
        }
        public FieldTripAdditionalDetailViewModel(string type)
        {
            Type = type;
        }
        //public int? TripId { get; set; }
        public int? TripDetailsId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Detail Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Detail Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [DisplayName("Detail Type")]
        public string? Type { get; set; }
    }
}
