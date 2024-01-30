using BAL.ViewModels.Event;

namespace WebAppMVC.Models.Meeting
{
    public class GetListMeetingParticipation : DefaultResponseViewModel
    {
        public List<GetEventParticipation>? Data { get; set; }
    }
}
