namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripInclusionResponse : DefaultResponseViewModel<object>
    {
        public GetFieldTripInclusionResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
