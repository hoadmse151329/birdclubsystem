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
        [StringLength(100, ErrorMessage = "Item Title length must not be longer than {0} characters")]
        [MaxLength(100, ErrorMessage = "Item Title length must not be longer than {0} characters")]
        [DisplayName("Item title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Item details")]
        public string? InclusionText { get; set; }
        [Required(ErrorMessage = "Item Type is required")]
        [StringLength(50, ErrorMessage = "Item Type length must not be longer than {0} characters")]
        [MaxLength(50,ErrorMessage = "Item Type length must not be longer than {0} characters")]
        [DisplayName("Type of item")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Inclusion Type is required, either included or excluded")]
        [StringLength(50, ErrorMessage = "Inclusion Type length must not be longer than {0} characters")]
        [MaxLength(50, ErrorMessage = "Inclusion Type length must not be longer than {0} characters")]
        [DisplayName("Type of inclusion")]
        public string? Inclusiontype { get; set; }
    }
}
