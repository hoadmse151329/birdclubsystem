using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripResponseByList : DefaultResponseViewModel<List<FieldTripViewModel>>
    {
        public GetFieldTripResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
