namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingMediaResponse : DefaultResponseViewModel<object>
    {
        public GetMeetingMediaResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
