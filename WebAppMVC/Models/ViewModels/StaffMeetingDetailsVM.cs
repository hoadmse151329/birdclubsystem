using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class StaffMeetingDetailsVM
    {
        public StaffMeetingDetailsVM()
        {
            MeetingDetails = new();
            UpdateMeetingStatus = new();
            MeetingParticipants = new();
            ParticipantStatusSelectableList = new();
        }
        public BAL.ViewModels.MeetingViewModel MeetingDetails { get; set; }
        public BAL.ViewModels.Manager.UpdateMeetingStatusVM UpdateMeetingStatus { get; set; }
        public List<BAL.ViewModels.MeetingParticipantViewModel> MeetingParticipants { get; set; }
        public List<SelectListItem> ParticipantStatusSelectableList { get; set; }
    }
}
