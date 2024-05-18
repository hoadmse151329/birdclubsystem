using BAL.ViewModels;

namespace WebAppMVC.Models.Manager
{
    public class GetListMeetingStatus : DefaultResponseViewModel<List<MeetingViewModel>>
    {
        public GetListMeetingStatus(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
