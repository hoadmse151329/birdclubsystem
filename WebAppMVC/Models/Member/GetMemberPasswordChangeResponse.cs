namespace WebAppMVC.Models.Member
{
    public class GetMemberPasswordChangeResponse : DefaultResponseViewModel<object>
    {
        public GetMemberPasswordChangeResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
