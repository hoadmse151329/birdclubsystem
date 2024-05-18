using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
    public class GetContestPostResponse : DefaultResponseViewModel<ContestViewModel>
    {
        public GetContestPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
        public GetContestPostResponse()
        {
        }
    }
}
