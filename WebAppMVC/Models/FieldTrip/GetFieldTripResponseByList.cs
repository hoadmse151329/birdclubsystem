using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripResponseByList : DefaultResponseViewModel
    {
        public List<FieldTripViewModel>? Data { get; set; }
    }
}
