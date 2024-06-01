namespace WebAppMVC.Models.ViewModels
{
    public class ManagerBlogIndexVM
    {
        public ManagerBlogIndexVM()
        {
            Blogs = new List<BAL.ViewModels.BlogViewModel>();
            SelectedBlogsStatuses = new List<string>();
        }
        public List<BAL.ViewModels.BlogViewModel>? Blogs { get; set; }
        public List<string> SelectedBlogsStatuses { get; set; }
    }
}
