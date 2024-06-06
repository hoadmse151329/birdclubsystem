namespace WebAppMVC.Models.Contest
{
    public class GetContestMediaResponse : DefaultResponseViewModel<object>
    {
        public GetContestMediaResponse()
        {
        }

        public GetContestMediaResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
