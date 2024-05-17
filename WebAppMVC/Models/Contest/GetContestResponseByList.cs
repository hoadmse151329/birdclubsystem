using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
	public class GetContestResponseByList : DefaultResponseViewModel<List<ContestViewModel>>
	{
        public GetContestResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

        //public List<ContestViewModel>? Data { get; set; }
	}
}
