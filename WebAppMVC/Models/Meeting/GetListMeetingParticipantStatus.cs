using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetListMeetingParticipantStatus : DefaultResponseViewModel
    {
        public IEnumerable<MeetingParticipantViewModel> Data { get; set; }
    }
}
