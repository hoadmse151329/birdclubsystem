using BAL.ViewModels;

namespace WebAppMVC.Models.Contest
{
    public class GetContestPostResponse : DefaultResponseViewModel
    {
        public ContestViewModel? Data { get; set; }
    }
}
