using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
	public class GetContestResponseByList : DefaultResponseViewModel
	{
		public List<ContestViewModel> Data { get; set; }
	}
}
