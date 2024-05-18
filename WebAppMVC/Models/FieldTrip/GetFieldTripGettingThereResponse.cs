using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripGettingThereResponse : DefaultResponseViewModel<FieldtripGettingThereViewModel>
    {
        public GetFieldTripGettingThereResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
