using BAL.ViewModels;

namespace WebAppMVC.Models.Location
{
    public class GetLocationResponse : DefaultResponseViewModel
    {
        public LocationViewModel Data { get; set; }
    }
}
