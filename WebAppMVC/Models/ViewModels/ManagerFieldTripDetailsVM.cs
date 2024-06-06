using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerFieldTripDetailsVM
    {
        public ManagerFieldTripDetailsVM()
        {
            FieldTripDetails = new();
            UpdateFieldTrip = new();
            UpdateFieldTripStatus = new();
            FieldTripParticipants = new();
            CreateFieldTripMedia = new();
            UpdateFieldTripMediaSpotlight = new();
            UpdateFieldTripMediaLocationMap = new();
            UpdateFieldTripMediaAdditional = new();
        }
        public BAL.ViewModels.FieldtripMediaViewModel CreateFieldTripMedia { get; set; }
        public BAL.ViewModels.FieldtripMediaViewModel UpdateFieldTripMediaSpotlight { get; set; }
        public BAL.ViewModels.FieldtripMediaViewModel UpdateFieldTripMediaLocationMap { get; set; }
        public List<BAL.ViewModels.FieldtripMediaViewModel> UpdateFieldTripMediaAdditional { get; set; }
        public BAL.ViewModels.FieldTripViewModel FieldTripDetails { get; set; }
        public BAL.ViewModels.Manager.UpdateFieldtripDetailsVM UpdateFieldTrip { get; set; }
        public BAL.ViewModels.Manager.UpdateFieldtripStatusVM UpdateFieldTripStatus { get; set; }
        public List<BAL.ViewModels.FieldTripParticipantViewModel> FieldTripParticipants { get; set; }
        public void SetIfUpdateFieldTripDetails(
            BAL.ViewModels.Manager.UpdateFieldtripDetailsVM updateFieldTripFirstResult,
            BAL.ViewModels.Manager.UpdateFieldtripDetailsVM updateFieldTripSecondResult,
            List<SelectListItem> meetingStatusSelectableList,
            List<SelectListItem> meetingStaffNamesSelectableList
            )
        {
            UpdateFieldTrip = updateFieldTripFirstResult != null ? updateFieldTripFirstResult :
                            updateFieldTripSecondResult != null ? updateFieldTripSecondResult : new();
            UpdateFieldTrip.FieldtripStaffNames = meetingStaffNamesSelectableList;
            UpdateFieldTrip.FieldtripStatusSelectableList = meetingStatusSelectableList;
        }
        public void SetIfUpdateFieldTripStatus(
            BAL.ViewModels.Manager.UpdateFieldtripStatusVM updateFieldTripStatusFirstResult,
            BAL.ViewModels.FieldTripViewModel updateFieldTripStatusSecondResult,
            List<SelectListItem> meetingStatusSelectableList
            )
        {
            UpdateFieldTripStatus = updateFieldTripStatusFirstResult != null ? updateFieldTripStatusFirstResult : new()
            {
                TripId = updateFieldTripStatusSecondResult.TripId,
                NumberOfParticipants = updateFieldTripStatusSecondResult.NumberOfParticipants,
                Status = updateFieldTripStatusSecondResult.Status,
                FieldtripStatusSelectableList = meetingStatusSelectableList
            };
        }
        public void SetIfCreateFieldTripMedia(BAL.ViewModels.FieldtripMediaViewModel createFieldTripMedia)
        {
            CreateFieldTripMedia = createFieldTripMedia != null ? createFieldTripMedia : new();
        }
        public void SetIfUpdateFieldTripMediaSpotlight(
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaSpotlightFirstResult,
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaSpotlightSecondResult
            )
        {
            UpdateFieldTripMediaSpotlight = updateFieldTripMediaSpotlightFirstResult != null ? updateFieldTripMediaSpotlightFirstResult :
                                          updateFieldTripMediaSpotlightSecondResult != null ? updateFieldTripMediaSpotlightSecondResult : new();
        }
        public void SetIfUpdateFieldTripMediaLocationMap(
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaLocationMapFirstResult,
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaLocationMapSecondResult
            )
        {
            UpdateFieldTripMediaLocationMap = updateFieldTripMediaLocationMapFirstResult != null ? updateFieldTripMediaLocationMapFirstResult :
                                          updateFieldTripMediaLocationMapSecondResult != null ? updateFieldTripMediaLocationMapSecondResult : new();
        }
        public void SetIfUpdateFieldTripMediaAdditional(
            List<BAL.ViewModels.FieldtripMediaViewModel> meetingPictures,
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaAdditional
            )
        {
            if (updateFieldTripMediaAdditional != null)
            {
                var updateCMA = meetingPictures.FirstOrDefault(mm => mm.PictureId.Value.Equals(updateFieldTripMediaAdditional.PictureId));
                updateCMA = updateFieldTripMediaAdditional != null ? updateFieldTripMediaAdditional : updateCMA;
            }
            UpdateFieldTripMediaAdditional = meetingPictures;
        }
    }
}
