using BAL.ViewModels;

namespace WebAppMVC.Models.Member
{
    public class GetUserNotificationResponse : DefaultResponseViewModel<IEnumerable<NotificationViewModel>>
    {
        public GetUserNotificationResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
