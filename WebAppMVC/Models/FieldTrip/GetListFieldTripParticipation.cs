using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetListFieldTripParticipation : DefaultResponseViewModel<List<FieldTripParticipantViewModel>>
    {
        public GetListFieldTripParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
