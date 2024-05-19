namespace WebAppMVC.Models.Notification
{
    public class GetUserNotificationReadAll : DefaultResponseViewModel<object>
    {
        public GetUserNotificationReadAll(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
