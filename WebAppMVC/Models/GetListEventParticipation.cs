using BAL.ViewModels.Event;

namespace WebAppMVC.Models
{
    public class GetListEventParticipation : DefaultResponseViewModel
    {
        public List<GetEventParticipation>? Data { get; set; }
    }
}
