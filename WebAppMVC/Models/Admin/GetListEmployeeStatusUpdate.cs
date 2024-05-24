namespace WebAppMVC.Models.Admin
{
    public class GetListEmployeeStatusUpdate : DefaultResponseViewModel<object>
    {
        public GetListEmployeeStatusUpdate(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
