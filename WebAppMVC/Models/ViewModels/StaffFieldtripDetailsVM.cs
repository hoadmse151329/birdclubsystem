using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class StaffFieldtripDetailsVM
    {
        public StaffFieldtripDetailsVM()
        {
            FieldTripDetails = new();
            UpdateFieldtripStatus = new();
            FieldTripTourFeatures = new();
            FieldTripImportantToKnows = new();
            FieldTripActivitiesAndTransportation = new();
            FieldTripParticipants = new();
            ParticipantStatusSelectableList = new();
        }
        public void SetIfUpdateFieldTripStatus(
            BAL.ViewModels.Manager.UpdateFieldtripStatusVM updateFieldTripStatusFirstResult,
            BAL.ViewModels.FieldTripViewModel updateFieldTripStatusSecondResult,
            List<SelectListItem> fieldtripStatusSelectableList
            )
        {
            UpdateFieldtripStatus = updateFieldTripStatusFirstResult != null ? updateFieldTripStatusFirstResult : new()
            {
                TripId = updateFieldTripStatusSecondResult.TripId,
                NumberOfParticipants = updateFieldTripStatusSecondResult.NumberOfParticipants,
                Status = updateFieldTripStatusSecondResult.Status,
                FieldtripStatusSelectableList = fieldtripStatusSelectableList
            };
        }
        public BAL.ViewModels.FieldTripViewModel? FieldTripDetails {  get; set; }
        public BAL.ViewModels.Manager.UpdateFieldtripStatusVM UpdateFieldtripStatus { get; set; }
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel>? FieldTripTourFeatures { get; set;}
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel>? FieldTripImportantToKnows { get; set; }
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel>? FieldTripActivitiesAndTransportation { get; set; }
        public List<BAL.ViewModels.FieldTripParticipantViewModel>? FieldTripParticipants { get; set; }
        public List<SelectListItem> ParticipantStatusSelectableList { get; set; }
    }
}
