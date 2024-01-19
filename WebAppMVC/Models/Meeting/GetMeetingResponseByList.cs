using BAL.ViewModels;

namespace WebAppMVC.Models.Meeting
{
	public class GetMeetingResponseByList : DefaultResponseViewModel
	{
		public List<MeetingViewModel>? Data { get; set; }
	}
}
