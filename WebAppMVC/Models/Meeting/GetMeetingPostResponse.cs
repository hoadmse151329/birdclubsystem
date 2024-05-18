using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingPostResponse : DefaultResponseViewModel<MeetingViewModel>
    {
        public GetMeetingPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

        public GetMeetingPostResponse()
        {

        }
    }
}
