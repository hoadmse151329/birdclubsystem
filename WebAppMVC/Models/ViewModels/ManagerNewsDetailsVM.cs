namespace WebAppMVC.Models.ViewModels
{
    public class ManagerNewsDetailsVM
    {
        public ManagerNewsDetailsVM()
        {
            News = new BAL.ViewModels.NewsViewModel();
            //SelectedNewsStatuses = new List<string>();
            updateNews = new BAL.ViewModels.NewsViewModel();
        }
        public BAL.ViewModels.NewsViewModel? News { get; set; }
        //public List<string> SelectedNewsStatuses { get; set; }
        public BAL.ViewModels.NewsViewModel? updateNews { get; set; }
    }
}
