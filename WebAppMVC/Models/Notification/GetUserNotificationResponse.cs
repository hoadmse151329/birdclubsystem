using BAL.ViewModels;

namespace WebAppMVC.Models.Member
{
    public class GetUserNotificationResponse : DefaultResponseViewModel
    {
        public IEnumerable<NotificationViewModel>? Data { get; set; }
    }
}
