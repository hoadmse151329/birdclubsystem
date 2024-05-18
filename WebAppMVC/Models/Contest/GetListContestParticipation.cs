using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
    public class GetListContestParticipation : DefaultResponseViewModel<List<ContestParticipantViewModel>>
    {
        public GetListContestParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
