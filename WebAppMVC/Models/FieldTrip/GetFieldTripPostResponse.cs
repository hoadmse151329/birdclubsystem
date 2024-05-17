using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripPostResponse : DefaultResponseViewModel<FieldTripViewModel>
    {
        public GetFieldTripPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
        public GetFieldTripPostResponse()
        {
        }
    }
}
