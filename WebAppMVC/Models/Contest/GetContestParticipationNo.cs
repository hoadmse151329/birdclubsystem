namespace WebAppMVC.Models.Contest
{
    public class GetContestParticipationNo : DefaultResponseViewModel<object>
    {
        public GetContestParticipationNo(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
