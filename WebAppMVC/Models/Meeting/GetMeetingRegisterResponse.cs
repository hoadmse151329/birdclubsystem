using BAL.ViewModels;
using BAL.ViewModels.Meeting;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingRegisterResponse
    {
        public bool Status { get; set; }
        public RegisterMeeting Data { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
