namespace WebAppMVC.Models.ViewModels
{
    public class ManagerBlogDetailsVM
    {
        public ManagerBlogDetailsVM()
        {
            Blog = new BAL.ViewModels.BlogViewModel();
            //SelectedNewsStatuses = new List<string>();
            updateBlog = new BAL.ViewModels.Blog.UpdateBlogDetails();
            updateBlogStatus = new BAL.ViewModels.Blog.UpdateBlogStatus();
        }
        public BAL.ViewModels.BlogViewModel? Blog { get; set; }
        //public List<string> SelectedNewsStatuses { get; set; }
        public BAL.ViewModels.Blog.UpdateBlogDetails? updateBlog { get; set; }
        public BAL.ViewModels.Blog.UpdateBlogStatus? updateBlogStatus { get; set; }
    }
}
