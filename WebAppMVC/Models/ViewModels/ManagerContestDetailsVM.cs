using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerContestDetailsVM
    {
        public ManagerContestDetailsVM()
        {
            ContestDetails = new();
            UpdateContest = new();
            UpdateContestStatus = new();
            ContestParticipants = new();
            CreateContestMedia = new();
            UpdateContestMediaSpotlight = new();
            UpdateContestMediaLocationMap = new();
            UpdateContestMediaAdditional = new();
        }
        public BAL.ViewModels.ContestViewModel ContestDetails {  get; set; }
        public BAL.ViewModels.Manager.UpdateContestDetailsVM UpdateContest {  get; set; }
        public BAL.ViewModels.Manager.UpdateContestStatusVM UpdateContestStatus { get; set; }
        public List<BAL.ViewModels.ContestParticipantViewModel> ContestParticipants { get; set; }
        public BAL.ViewModels.ContestMediaViewModel CreateContestMedia { get; set; }
        public BAL.ViewModels.ContestMediaViewModel UpdateContestMediaSpotlight { get; set; }
        public BAL.ViewModels.ContestMediaViewModel UpdateContestMediaLocationMap { get; set; }
        public List<BAL.ViewModels.ContestMediaViewModel> UpdateContestMediaAdditional { get; set; }
        // 
        public void SetIfUpdateContestDetails(
            BAL.ViewModels.Manager.UpdateContestDetailsVM updateContestFirstResult, 
            BAL.ViewModels.Manager.UpdateContestDetailsVM updateContestSecondResult,
            List<SelectListItem> contestStatusSelectableList,
            List<SelectListItem> contestStaffNamesSelectableList
            )
        {
            UpdateContest = updateContestFirstResult != null ? updateContestFirstResult : 
                            updateContestSecondResult != null ? updateContestSecondResult : new();
            UpdateContest.ContestStaffNames = contestStaffNamesSelectableList;
            UpdateContest.ContestStatusSelectableList = contestStatusSelectableList;
        }
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
        public void SetIfCreateContestMedia(BAL.ViewModels.ContestMediaViewModel createContestMedia)
        {
            CreateContestMedia = createContestMedia != null ? createContestMedia : new();
        }
        public void SetIfUpdateContestMediaSpotlight(
            BAL.ViewModels.ContestMediaViewModel updateContestMediaSpotlightFirstResult,
            BAL.ViewModels.ContestMediaViewModel updateContestMediaSpotlightSecondResult
            )
        {
            UpdateContestMediaSpotlight = updateContestMediaSpotlightFirstResult != null ? updateContestMediaSpotlightFirstResult :
                                          updateContestMediaSpotlightSecondResult != null ? updateContestMediaSpotlightSecondResult : new();
        }
        public void SetIfUpdateContestMediaLocationMap(
            BAL.ViewModels.ContestMediaViewModel updateContestMediaLocationMapFirstResult,
            BAL.ViewModels.ContestMediaViewModel updateContestMediaLocationMapSecondResult
            )
        {
            UpdateContestMediaLocationMap = updateContestMediaLocationMapFirstResult != null ? updateContestMediaLocationMapFirstResult :
                                          updateContestMediaLocationMapSecondResult != null ? updateContestMediaLocationMapSecondResult : new();
        }
        public void SetIfUpdateContestMediaAdditional(
            List<BAL.ViewModels.ContestMediaViewModel> contestPictures,
            BAL.ViewModels.ContestMediaViewModel updateContestMediaAdditional
            )
        {
            if(updateContestMediaAdditional != null)
            {
                var updateCMA = contestPictures.FirstOrDefault(mm => mm.PictureId.Value.Equals(updateContestMediaAdditional.PictureId));
                updateCMA = updateContestMediaAdditional != null ? updateContestMediaAdditional : updateCMA;
            }
            UpdateContestMediaAdditional = contestPictures;
        }
    }
}
