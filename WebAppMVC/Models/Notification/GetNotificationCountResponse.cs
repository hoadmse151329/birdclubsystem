namespace WebAppMVC.Models.Notification
{
    public class GetNotificationCountResponse : DefaultResponseViewModel<object>
    {
        public GetNotificationCountResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
