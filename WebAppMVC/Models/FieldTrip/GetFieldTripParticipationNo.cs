namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripParticipationNo : DefaultResponseViewModel<object>
    {
        public GetFieldTripParticipationNo(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
