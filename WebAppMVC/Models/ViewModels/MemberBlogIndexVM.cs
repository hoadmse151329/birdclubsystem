namespace WebAppMVC.Models.ViewModels
{
    public class MemberBlogIndexVM
    {
        public MemberBlogIndexVM()
        {
            Blogs = new List<BAL.ViewModels.BlogViewModel>();
            createBlog = new BAL.ViewModels.Blog.CreateNewBlog();
            isGuest = true;
        }
        public List<BAL.ViewModels.BlogViewModel>? Blogs { get; set; }
        public BAL.ViewModels.Blog.CreateNewBlog createBlog { get; set; }
        public bool isGuest { get; set; }
    }
}
