using BAL.ViewModels;

namespace WebAppMVC.Models.Blog
{
    public class GetBlogPostResponse : DefaultResponseViewModel<BlogViewModel>
    {
        public GetBlogPostResponse()
        {
        }

        public GetBlogPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
