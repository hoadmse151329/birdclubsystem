using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
	public class GetMeetingResponseByList : DefaultResponseViewModel<List<MeetingViewModel>>
	{
        public GetMeetingResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
	}
}
