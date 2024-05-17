namespace WebAppMVC.Models.Staff
{
    public class GetCheckInStatusUpdate : DefaultResponseViewModel<object>
    {
        public GetCheckInStatusUpdate(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
