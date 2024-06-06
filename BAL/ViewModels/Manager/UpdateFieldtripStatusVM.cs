using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Manager
{
    public class UpdateFieldtripStatusVM
    {
        public UpdateFieldtripStatusVM()
        {
            Status = "OnHold";
            NumberOfParticipants = 0;
        }
        public int? TripId { get; set; }
        [DisplayName("Number Of Participants")]
        public int NumberOfParticipants { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public List<SelectListItem>? FieldtripStatusSelectableList { get; set; }
    }
}
