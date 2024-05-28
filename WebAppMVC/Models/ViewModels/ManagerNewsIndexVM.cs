namespace WebAppMVC.Models.ViewModels
{
    public class ManagerNewsIndexVM
    {
        public ManagerNewsIndexVM()
        {
            News = new List<BAL.ViewModels.NewsViewModel>();
            SelectedNewsStatuses = new List<string>();
            createNews = new BAL.ViewModels.News.CreateNewNews();
        }
        public List<BAL.ViewModels.NewsViewModel>? News { get; set; }
        public List<string> SelectedNewsStatuses { get; set; }
        public BAL.ViewModels.News.CreateNewNews createNews { get; set; }
    }
}
