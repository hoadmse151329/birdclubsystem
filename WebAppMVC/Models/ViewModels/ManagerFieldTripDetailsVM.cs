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
            UpdateFieldTripGettingThere = new();
            UpdateFieldTripDayByDay = new();
            UpdateFieldTripInclusion = new();
            UpdateFieldTripTourFeature = new();
            UpdateFieldTripImportant = new();
            UpdateFieldTripActAndTras = new();

            FieldTripParticipants = new();
            FieldTripDayByDays = new();
            FieldTripInclusions = new();
            FieldTripTourFeatures = new();
            FieldTripImportants = new();
            FieldTripActAndTrass = new();

            CreateFieldTripMedia = new();
            CreateFieldTripDayByDay = new();
            CreateFieldTripInclusion = new();
            CreateFieldTripTourFeature = new();
            CreateFieldTripImportant = new();
            CreateFieldTripActAndTras = new();

            UpdateFieldTripMediaSpotlight = new();
            UpdateFieldTripMediaLocationMap = new();
            UpdateFieldTripMediaAdditional = new();

        }
        public BAL.ViewModels.FieldtripMediaViewModel CreateFieldTripMedia { get; set; }
        public BAL.ViewModels.FieldtripDaybyDayViewModel CreateFieldTripDayByDay { get; set; }
        public BAL.ViewModels.FieldtripInclusionViewModel CreateFieldTripInclusion { get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel CreateFieldTripTourFeature { get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel CreateFieldTripImportant { get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel CreateFieldTripActAndTras { get; set; }
        public BAL.ViewModels.FieldtripMediaViewModel UpdateFieldTripMediaSpotlight { get; set; }
        public BAL.ViewModels.FieldtripMediaViewModel UpdateFieldTripMediaLocationMap { get; set; }
        public BAL.ViewModels.FieldtripMediaViewModel UpdateFieldTripMediaAdditional { get; set; }
        public BAL.ViewModels.Manager.UpdateFieldtripDetailsVM UpdateFieldTrip { get; set; }
        public BAL.ViewModels.FieldtripGettingThereViewModel UpdateFieldTripGettingThere { get; set; }
        public BAL.ViewModels.Manager.UpdateFieldtripStatusVM UpdateFieldTripStatus { get; set; }
        public BAL.ViewModels.FieldtripDaybyDayViewModel UpdateFieldTripDayByDay { get; set; }
        public BAL.ViewModels.FieldtripInclusionViewModel UpdateFieldTripInclusion { get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel UpdateFieldTripTourFeature {  get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel UpdateFieldTripImportant { get; set; }
        public BAL.ViewModels.FieldTripAdditionalDetailViewModel UpdateFieldTripActAndTras { get; set; }
        public BAL.ViewModels.FieldTripViewModel FieldTripDetails { get; set; }
        public List<BAL.ViewModels.FieldtripDaybyDayViewModel> FieldTripDayByDays { get; set; }
        public List<BAL.ViewModels.FieldTripParticipantViewModel> FieldTripParticipants { get; set; }
        public List<BAL.ViewModels.FieldtripInclusionViewModel> FieldTripInclusions { get; set; }
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel> FieldTripTourFeatures { get; set; }
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel> FieldTripImportants { get; set; }
        public List<BAL.ViewModels.FieldTripAdditionalDetailViewModel> FieldTripActAndTrass { get; set; }
        public void SetIfUpdateFieldTripDetails(
            BAL.ViewModels.Manager.UpdateFieldtripDetailsVM updateFieldTripFirstResult,
            BAL.ViewModels.Manager.UpdateFieldtripDetailsVM updateFieldTripSecondResult,
            List<SelectListItem> fieldtripStatusSelectableList,
            List<SelectListItem> fieldtripStaffNamesSelectableList
            )
        {
            UpdateFieldTrip = updateFieldTripFirstResult != null ? updateFieldTripFirstResult :
                            updateFieldTripSecondResult != null ? updateFieldTripSecondResult : new();
            UpdateFieldTrip.FieldtripStaffNames = fieldtripStaffNamesSelectableList;
            UpdateFieldTrip.FieldtripStatusSelectableList = fieldtripStatusSelectableList;
        }
        public void SetIfUpdateFieldTripStatus(
            BAL.ViewModels.Manager.UpdateFieldtripStatusVM updateFieldTripStatusFirstResult,
            BAL.ViewModels.FieldTripViewModel updateFieldTripStatusSecondResult,
            List<SelectListItem> fieldtripStatusSelectableList
            )
        {
            UpdateFieldTripStatus = updateFieldTripStatusFirstResult != null ? updateFieldTripStatusFirstResult : new()
            {
                TripId = updateFieldTripStatusSecondResult.TripId,
                NumberOfParticipants = updateFieldTripStatusSecondResult.NumberOfParticipants,
                Status = updateFieldTripStatusSecondResult.Status,
                FieldtripStatusSelectableList = fieldtripStatusSelectableList
            };
        }
        public void SetIfCreateFieldTripMedia(BAL.ViewModels.FieldtripMediaViewModel createFieldTripMedia)
        {
            CreateFieldTripMedia = createFieldTripMedia != null ? createFieldTripMedia : new();
        }
        public void SetIfCreateFieldTripDayByDay(BAL.ViewModels.FieldtripDaybyDayViewModel createFieldTripDayByDay)
        {
            CreateFieldTripDayByDay = createFieldTripDayByDay != null ? createFieldTripDayByDay : new();
        }
        public void SetIfCreateFieldTripInclusion(
            BAL.ViewModels.FieldtripInclusionViewModel createFieldTripInclusion,
            List<SelectListItem> fieldtripInclusionTypeSelectableList
            )
        {
            CreateFieldTripInclusion = createFieldTripInclusion != null ? createFieldTripInclusion : new();
            CreateFieldTripInclusion.InclusionTypesSelectableList = fieldtripInclusionTypeSelectableList;
        }
        public void SetIfCreateFieldTripTourFeature(BAL.ViewModels.FieldTripAdditionalDetailViewModel createFieldTripTourFeature)
        {
            CreateFieldTripTourFeature = createFieldTripTourFeature != null ? createFieldTripTourFeature : new("tour_features");
        }
        public void SetIfCreateFieldTripImportant(BAL.ViewModels.FieldTripAdditionalDetailViewModel createFieldTripImportant)
        {
            CreateFieldTripImportant = createFieldTripImportant != null ? createFieldTripImportant : new("important_to_know");
        }
        public void SetIfCreateFieldTripActAndTras(BAL.ViewModels.FieldTripAdditionalDetailViewModel createFieldTripActAndTras)
        {
            CreateFieldTripActAndTras = createFieldTripActAndTras != null ? createFieldTripActAndTras : new("activities_and_transportation");
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
        public void SetIfUpdateFieldTripGettingThere(
            BAL.ViewModels.FieldtripGettingThereViewModel updateFieldTripGettingThereFirstResult,
            BAL.ViewModels.FieldtripGettingThereViewModel updateFieldTripGettingThereSecondResult
            )
        {
            UpdateFieldTripGettingThere = updateFieldTripGettingThereFirstResult != null ? updateFieldTripGettingThereFirstResult :
                                          updateFieldTripGettingThereSecondResult != null ? updateFieldTripGettingThereSecondResult : new();
        }
        public void SetIfUpdateFieldTripMediaAdditional(
            BAL.ViewModels.FieldtripMediaViewModel updateFieldTripMediaAdditional
            )
        {
            UpdateFieldTripMediaAdditional = updateFieldTripMediaAdditional != null ? updateFieldTripMediaAdditional : new();
        }
        public void SetIfUpdateFieldTripDayByDay(
            BAL.ViewModels.FieldtripDaybyDayViewModel updateFieldTripDayByDay
            )
        {
            UpdateFieldTripDayByDay = updateFieldTripDayByDay != null ? updateFieldTripDayByDay : new();
        }
        public void SetIfUpdateFieldTripInclusion(
            BAL.ViewModels.FieldtripInclusionViewModel updateFieldTripInclusion,
            List<SelectListItem> fieldtripInclusionTypeSelectableList
            )
        {
            UpdateFieldTripInclusion = updateFieldTripInclusion != null ? updateFieldTripInclusion : new();
            UpdateFieldTripInclusion.InclusionTypesSelectableList = fieldtripInclusionTypeSelectableList;
        }
        public void SetIfUpdateFieldTripTourFeature(
            BAL.ViewModels.FieldTripAdditionalDetailViewModel updateFieldTripTourFeature
            )
        {
            UpdateFieldTripTourFeature = updateFieldTripTourFeature != null ? updateFieldTripTourFeature : new("tour_features");
        }
        public void SetIfUpdateFieldTripImportant(
            BAL.ViewModels.FieldTripAdditionalDetailViewModel updateFieldTripImportant
            )
        {
            UpdateFieldTripImportant = updateFieldTripImportant != null ? updateFieldTripImportant : new("important_to_know");
        }
        public void SetIfUpdateFieldTripActAndTras(
            BAL.ViewModels.FieldTripAdditionalDetailViewModel updateFieldTripActAndTras
            )
        {
            UpdateFieldTripActAndTras = updateFieldTripActAndTras != null ? updateFieldTripActAndTras : new("activities_and_transportation");
        }
    }
}
