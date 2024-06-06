using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerMeetingDetailsVM
    {
        public ManagerMeetingDetailsVM()
        {
            MeetingDetails = new();
            UpdateMeeting = new();
            UpdateMeetingStatus = new();
            MeetingParticipants = new();
            CreateMeetingMedia = new();
            UpdateMeetingMediaSpotlight = new();
            UpdateMeetingMediaLocationMap = new();
            UpdateMeetingMediaAdditional = new();
        }
        public BAL.ViewModels.MeetingMediaViewModel CreateMeetingMedia { get; set; }
        public BAL.ViewModels.MeetingMediaViewModel UpdateMeetingMediaSpotlight { get; set; }
        public BAL.ViewModels.MeetingMediaViewModel UpdateMeetingMediaLocationMap { get; set; }
        public List<BAL.ViewModels.MeetingMediaViewModel> UpdateMeetingMediaAdditional { get; set; }
        public BAL.ViewModels.MeetingViewModel MeetingDetails { get; set; }
        public BAL.ViewModels.Manager.UpdateMeetingDetailsVM UpdateMeeting { get; set; }
        public BAL.ViewModels.Manager.UpdateMeetingStatusVM UpdateMeetingStatus { get; set; }
        public List<BAL.ViewModels.MeetingParticipantViewModel> MeetingParticipants {  get; set; }
    }
}
