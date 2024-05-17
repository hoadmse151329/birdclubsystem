using BAL.ViewModels;

namespace WebAppMVC.Models.Notification
{
    public class GetNotificationPostResponse : DefaultResponseViewModel
    {
        public NotificationViewModel? Data { get; set; }
    }
}
