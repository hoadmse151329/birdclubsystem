namespace WebAppMVC.Models.Meeting
{
    public class GetMeetingPostDeRegister : DefaultResponseViewModel<object>
    {
        public GetMeetingPostDeRegister(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
