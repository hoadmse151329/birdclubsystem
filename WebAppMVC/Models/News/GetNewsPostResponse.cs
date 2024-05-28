using BAL.ViewModels;

namespace WebAppMVC.Models.News
{
    public class GetNewsPostResponse : DefaultResponseViewModel<NewsViewModel>
    {
        public GetNewsPostResponse()
        {
        }

        public GetNewsPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
