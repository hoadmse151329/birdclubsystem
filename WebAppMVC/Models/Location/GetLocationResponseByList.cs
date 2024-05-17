using BAL.ViewModels;

namespace WebAppMVC.Models.Location
{
    public class GetLocationResponseByList : DefaultResponseViewModel<List<LocationViewModel>>
    {
        public GetLocationResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
