using BAL.Attributes;
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
    public class UpdateMeetingStatusVM
    {
        public UpdateMeetingStatusVM()
        {
            Status = "OnHold";
            NumberOfParticipants = 0;
        }
        public int? MeetingId { get; set; }
        [DisplayName("Number Of Participants")]
        public int NumberOfParticipants { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public string? Status { get; set; }
        public List<SelectListItem>? MeetingStatusSelectableList { get; set; }
    }
}
