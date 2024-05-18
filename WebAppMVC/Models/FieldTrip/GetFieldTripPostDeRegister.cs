namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripPostDeRegister : DefaultResponseViewModel<object>
    {
        public GetFieldTripPostDeRegister(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
