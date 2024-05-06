using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FieldtripInclusionViewModel
    {
        //public int? TripId { get; set; }
        public int? InclusionId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Item Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Item details")]
        public string? InclusionText { get; set; }
        [Required(ErrorMessage = "Day Number value is required")]
        [DisplayName("Type of item")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Day Number value is required")]
        [DisplayName("Type of inclusion")]
        public string? Inclusiontype { get; set; }
    }
}
