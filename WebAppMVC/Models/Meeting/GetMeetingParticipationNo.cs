namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingParticipationNo : DefaultResponseViewModel<object>
    {
        public GetMeetingParticipationNo(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
