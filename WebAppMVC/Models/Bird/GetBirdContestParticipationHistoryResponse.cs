using BAL.ViewModels.Bird;

namespace WebAppMVC.Models.Bird
{
    public class GetBirdContestParticipationHistoryResponse : DefaultResponseViewModel<List<GetBirdContestParticipantDetail>>
    {
        public GetBirdContestParticipationHistoryResponse()
        {
        }

        public GetBirdContestParticipationHistoryResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
