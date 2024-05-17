namespace WebAppMVC.Models.Notification
{
    public class GetNotificationTitleResponse : DefaultResponseViewModel
    {
        public IEnumerable<string>? Data { get; set; }
    }
}
