using BAL.ViewModels;

namespace WebAppMVC.Models.Notification
{
    public class GetNotificationPostResponse : DefaultResponseViewModel<NotificationViewModel>
    {
        public GetNotificationPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
