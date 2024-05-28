namespace WebAppMVC.Models.ViewModels
{
    public class ManagerNewsDetailsVM
    {
        public ManagerNewsDetailsVM()
        {
            News = new BAL.ViewModels.NewsViewModel();
            //SelectedNewsStatuses = new List<string>();
            updateNews = new BAL.ViewModels.News.UpdateNewsDetail();
        }
        public BAL.ViewModels.NewsViewModel? News { get; set; }
        //public List<string> SelectedNewsStatuses { get; set; }
        public BAL.ViewModels.News.UpdateNewsDetail? updateNews { get; set; }
    }
}
