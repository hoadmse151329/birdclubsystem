using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripDayByDayResponse : DefaultResponseViewModel<object>
    {
        public GetFieldTripDayByDayResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
