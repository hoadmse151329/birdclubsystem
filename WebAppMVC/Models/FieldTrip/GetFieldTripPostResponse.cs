using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripPostResponse : DefaultResponseViewModel
    {
        public FieldTripViewModel? Data { get; set; }
    }
}
