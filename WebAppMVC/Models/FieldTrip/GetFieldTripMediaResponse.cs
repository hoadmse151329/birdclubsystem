namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripMediaResponse : DefaultResponseViewModel<object>
    {
        public GetFieldTripMediaResponse()
        {
        }

        public GetFieldTripMediaResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
