using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingPostResponse
    {
        public bool Status { get; set; }
        public MeetingViewModel Data { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
