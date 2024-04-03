using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
    public class GetListContestParticipation : DefaultResponseViewModel
    {
        public List<ContestParticipantViewModel>? Data { get; set; }
    }
}
