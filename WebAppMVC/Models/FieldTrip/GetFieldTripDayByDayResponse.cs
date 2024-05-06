using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetFieldTripDayByDayResponse : DefaultResponseViewModel
    {
        public FieldtripDaybyDayViewModel? Data { get; set; }
    }
}
