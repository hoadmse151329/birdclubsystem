using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingParticipantResponse : DefaultResponseViewModel<MeetingParticipantViewModel>
    {
        public GetMeetingParticipantResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
