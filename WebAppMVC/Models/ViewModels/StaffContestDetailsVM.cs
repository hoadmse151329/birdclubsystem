using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class StaffContestDetailsVM
    {
        public StaffContestDetailsVM()
        {
            ContestDetails = new();
            UpdateContestStatus = new();
            ContestParticipants = new();
            ParticipantStatusSelectableList = new();
        }
        public BAL.ViewModels.ContestViewModel? ContestDetails { get; set; }
        public BAL.ViewModels.Manager.UpdateContestStatusVM UpdateContestStatus { get; set; }
        public List<BAL.ViewModels.ContestParticipantViewModel> ContestParticipants { get; set; }
        public List<SelectListItem> ParticipantStatusSelectableList { get; set; }
        public void SetIfUpdateContestStatus(
            BAL.ViewModels.Manager.UpdateContestStatusVM updateContestStatusFirstResult,
            BAL.ViewModels.ContestViewModel updateContestStatusSecondResult,
            List<SelectListItem> contestStatusSelectableList
            )
        {
            UpdateContestStatus = updateContestStatusFirstResult != null ? updateContestStatusFirstResult : new()
            {
                ContestId = updateContestStatusSecondResult.ContestId,
                NumberOfParticipants = updateContestStatusSecondResult.NumberOfParticipants,
                Status = updateContestStatusSecondResult.Status,
                ContestStatusSelectableList = contestStatusSelectableList
            };
        }
    }
}
