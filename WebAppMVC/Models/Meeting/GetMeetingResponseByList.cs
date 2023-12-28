using BAL.ViewModels;
using BAL.ViewModels.Authenticates;

namespace WebAppMVC.Models.Meeting
{
	public class GetMeetingResponseByList
	{
		public bool Status { get; set; }
		public List<MeetingViewModel> Data { get; set; }
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}
}
