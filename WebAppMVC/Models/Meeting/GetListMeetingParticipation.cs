using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
    public class GetListMeetingParticipation : DefaultResponseViewModel<List<MeetingParticipantViewModel>>
    {
        public GetListMeetingParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
