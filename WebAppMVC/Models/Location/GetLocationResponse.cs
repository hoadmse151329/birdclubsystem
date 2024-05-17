using BAL.ViewModels;

namespace WebAppMVC.Models.Location
{
    public class GetLocationResponse : DefaultResponseViewModel<LocationViewModel>
    {
        public GetLocationResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
