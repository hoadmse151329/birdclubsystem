namespace WebAppMVC.Models.Notification
{
    public class GetNotificationTitleResponse : DefaultResponseViewModel<IEnumerable<string>>
    {
        public GetNotificationTitleResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
