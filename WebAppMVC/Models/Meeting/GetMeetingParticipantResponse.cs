using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingParticipantResponse : DefaultResponseViewModel
    {
        public MeetingParticipantViewModel? Data { get; set; }
    }
}
