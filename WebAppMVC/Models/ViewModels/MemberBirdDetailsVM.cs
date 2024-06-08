using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class MemberBirdDetailsVM
    {
        public MemberBirdDetailsVM()
        {
            MemberBirdDetails = new();
            BirdContestParticipationHistoryList = new();
            UpdateBirdDetails = new();
        }
        public BAL.ViewModels.BirdViewModel? MemberBirdDetails {  get; set; }
        public List<BAL.ViewModels.Bird.GetBirdContestParticipantDetail>? BirdContestParticipationHistoryList { get; set; }
        public BAL.ViewModels.BirdViewModel? UpdateBirdDetails { get; set; }

        public void SetIfUpdateBirdDetails(
            BAL.ViewModels.BirdViewModel updateBirdDetailsFirstResult,
            BAL.ViewModels.BirdViewModel updateBirdDetailsSecondResult,
            List<SelectListItem> birdStatusSelectableList
            )
        {
            UpdateBirdDetails = updateBirdDetailsFirstResult != null ? updateBirdDetailsFirstResult :
                            updateBirdDetailsSecondResult != null ? updateBirdDetailsSecondResult : new();
            UpdateBirdDetails.DefaultBirdStatusSelectList = birdStatusSelectableList;
        }
    }
}
