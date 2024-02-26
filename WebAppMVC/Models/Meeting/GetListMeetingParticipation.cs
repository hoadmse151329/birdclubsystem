using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetListMeetingParticipation : DefaultResponseViewModel
    {
        public List<MeetingParticipantViewModel>? Data { get; set; }
    }
}
