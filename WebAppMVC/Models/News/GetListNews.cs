using BAL.ViewModels;

namespace WebAppMVC.Models.News
{
    public class GetListNews : DefaultResponseViewModel<List<NewsViewModel>>
    {
        public GetListNews()
        {
        }

        public GetListNews(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
