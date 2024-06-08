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

        public void SetIfUpdateMeetingStatus(
            BAL.ViewModels.Manager.UpdateMeetingStatusVM updateMeetingStatusFirstResult,
            BAL.ViewModels.MeetingViewModel updateMeetingStatusSecondResult,
            List<SelectListItem> meetingStatusSelectableList
            )
        {
            UpdateMeetingStatus = updateMeetingStatusFirstResult != null ? updateMeetingStatusFirstResult : new()
            {
                MeetingId = updateMeetingStatusSecondResult.MeetingId,
                NumberOfParticipants = updateMeetingStatusSecondResult.NumberOfParticipants,
                Status = updateMeetingStatusSecondResult.Status,
                MeetingStatusSelectableList = meetingStatusSelectableList
            };
        }
    }
}
