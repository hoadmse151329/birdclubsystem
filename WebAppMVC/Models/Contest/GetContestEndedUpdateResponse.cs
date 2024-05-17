namespace WebAppMVC.Models.Contest
{
    public class GetContestEndedUpdateResponse : DefaultResponseViewModel<object>
    {
        public GetContestEndedUpdateResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
