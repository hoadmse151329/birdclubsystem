namespace WebAppMVC.Models.ViewModels
{
    public class ManagerBlogIndexVM
    {
        public ManagerBlogIndexVM()
        {
            Blogs = new List<BAL.ViewModels.BlogViewModel>();
            SelectedBlogsStatuses = new List<string>();
            createBlog = new BAL.ViewModels.BlogViewModel();
        }
        public List<BAL.ViewModels.BlogViewModel>? Blogs { get; set; }
        public List<string> SelectedBlogsStatuses { get; set; }
        public BAL.ViewModels.BlogViewModel createBlog { get; set; }
    }
}
