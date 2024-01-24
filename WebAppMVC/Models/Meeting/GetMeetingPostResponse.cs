using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingPostResponse : DefaultResponseViewModel
    {
        public MeetingViewModel? Data { get; set; }
    }
}
