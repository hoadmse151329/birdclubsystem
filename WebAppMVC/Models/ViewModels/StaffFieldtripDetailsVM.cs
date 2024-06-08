using BAL.ViewModels;
using BAL.ViewModels.Manager;
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
        public FieldTripViewModel? FieldTripDetails {  get; set; }
        public UpdateFieldtripStatusVM UpdateFieldtripStatus { get; set; }
        public List<FieldTripAdditionalDetailViewModel>? FieldTripTourFeatures { get; set;}
        public List<FieldTripAdditionalDetailViewModel>? FieldTripImportantToKnows { get; set; }
        public List<FieldTripAdditionalDetailViewModel>? FieldTripActivitiesAndTransportation { get; set; }
        public List<FieldTripParticipantViewModel>? FieldTripParticipants { get; set; }
        public List<SelectListItem> ParticipantStatusSelectableList { get; set; }
    }
}
