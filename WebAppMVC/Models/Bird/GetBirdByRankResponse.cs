using BAL.ViewModels.Event;

namespace WebAppMVC.Models.Bird
{
    public class GetBirdByRankResponse : DefaultResponseViewModel<List<GetLeaderboardResponse>>
    {
        public GetBirdByRankResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {  
        }
    }
}
