using BAL.ViewModels;

namespace WebAppMVC.Models.Blog
{
    public class GetListBlogResponse : DefaultResponseViewModel<List<BlogViewModel>>
    {
        public GetListBlogResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
