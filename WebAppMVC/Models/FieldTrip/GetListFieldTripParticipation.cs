using BAL.ViewModels;

namespace WebAppMVC.Models.FieldTrip
{
    public class GetListFieldTripParticipation : DefaultResponseViewModel
    {
        public List<FieldTripParticipantViewModel>? Data { get; set; }
    }
}
