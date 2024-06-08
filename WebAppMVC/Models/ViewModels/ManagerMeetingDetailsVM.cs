using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerMeetingDetailsVM
    {
        public ManagerMeetingDetailsVM()
        {
            MeetingDetails = new();
            UpdateMeeting = new();
            UpdateMeetingStatus = new();
            MeetingParticipants = new();
            CreateMeetingMedia = new();
            UpdateMeetingMediaSpotlight = new();
            UpdateMeetingMediaLocationMap = new();
            UpdateMeetingMediaAdditional = new();
        }
        public BAL.ViewModels.MeetingMediaViewModel CreateMeetingMedia { get; set; }
        public BAL.ViewModels.MeetingMediaViewModel UpdateMeetingMediaSpotlight { get; set; }
        public BAL.ViewModels.MeetingMediaViewModel UpdateMeetingMediaLocationMap { get; set; }
        public BAL.ViewModels.MeetingMediaViewModel UpdateMeetingMediaAdditional { get; set; }

        public BAL.ViewModels.MeetingViewModel MeetingDetails { get; set; }
        public BAL.ViewModels.Manager.UpdateMeetingDetailsVM UpdateMeeting { get; set; }
        public BAL.ViewModels.Manager.UpdateMeetingStatusVM UpdateMeetingStatus { get; set; }
        public List<BAL.ViewModels.MeetingParticipantViewModel> MeetingParticipants {  get; set; }
        public void SetIfUpdateMeetingDetails(
            BAL.ViewModels.Manager.UpdateMeetingDetailsVM updateMeetingFirstResult,
            BAL.ViewModels.Manager.UpdateMeetingDetailsVM updateMeetingSecondResult,
            List<SelectListItem> meetingStatusSelectableList,
            List<SelectListItem> meetingStaffNamesSelectableList
            )
        {
            UpdateMeeting = updateMeetingFirstResult != null ? updateMeetingFirstResult :
                            updateMeetingSecondResult != null ? updateMeetingSecondResult : new();
            UpdateMeeting.MeetingStaffNames = meetingStaffNamesSelectableList;
            UpdateMeeting.MeetingStatusSelectableList = meetingStatusSelectableList;
        }
        public void SetIfUpdateMeetingStatus(
            BAL.ViewModels.Manager.UpdateMeetingStatusVM updateMeetingStatusFirstResult,
            BAL.ViewModels.MeetingViewModel updateMeetingStatusSecondResult,
            List<SelectListItem> meetingStatusSelectableList
            )
        {
            UpdateMeetingStatus = updateMeetingStatusFirstResult != null ? updateMeetingStatusFirstResult : new()
            {
                MeetingId = updateMeetingStatusSecondResult.MeetingId,
                NumberOfParticipants = updateMeetingStatusSecondResult.NumberOfParticipants,
                Status = updateMeetingStatusSecondResult.Status,
                MeetingStatusSelectableList = meetingStatusSelectableList
            };
        }
        public void SetIfCreateMeetingMedia(BAL.ViewModels.MeetingMediaViewModel createMeetingMedia)
        {
            CreateMeetingMedia = createMeetingMedia != null ? createMeetingMedia : new();
        }
        public void SetIfUpdateMeetingMediaSpotlight(
            BAL.ViewModels.MeetingMediaViewModel updateMeetingMediaSpotlightFirstResult,
            BAL.ViewModels.MeetingMediaViewModel updateMeetingMediaSpotlightSecondResult
            )
        {
            UpdateMeetingMediaSpotlight = updateMeetingMediaSpotlightFirstResult != null ? updateMeetingMediaSpotlightFirstResult :
                                          updateMeetingMediaSpotlightSecondResult != null ? updateMeetingMediaSpotlightSecondResult : new();
        }
        public void SetIfUpdateMeetingMediaLocationMap(
            BAL.ViewModels.MeetingMediaViewModel updateMeetingMediaLocationMapFirstResult,
            BAL.ViewModels.MeetingMediaViewModel updateMeetingMediaLocationMapSecondResult
            )
        {
            UpdateMeetingMediaLocationMap = updateMeetingMediaLocationMapFirstResult != null ? updateMeetingMediaLocationMapFirstResult :
                                          updateMeetingMediaLocationMapSecondResult != null ? updateMeetingMediaLocationMapSecondResult : new();
        }
        public void SetIfUpdateMeetingMediaAdditional(
            BAL.ViewModels.MeetingMediaViewModel updateMeetingMediaAdditional
            )
        {
            UpdateMeetingMediaAdditional = updateMeetingMediaAdditional != null ? updateMeetingMediaAdditional : new();

            /*if (updateMeetingMediaAdditional != null)
            {
                var updateCMA = meetingPictures.FirstOrDefault(mm => mm.PictureId.Value.Equals(updateMeetingMediaAdditional.PictureId));
                updateCMA = updateMeetingMediaAdditional != null ? updateMeetingMediaAdditional : updateCMA;
            }
            UpdateMeetingMediaAdditional = meetingPictures;*/
        }
    }
}
