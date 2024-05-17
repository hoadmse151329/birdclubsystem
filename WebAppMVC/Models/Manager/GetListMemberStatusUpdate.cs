namespace WebAppMVC.Models.Manager
{
    public class GetListMemberStatusUpdate : DefaultResponseViewModel<object>
    {
        public GetListMemberStatusUpdate(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
