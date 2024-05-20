namespace WebAppMVC.Models.ViewModels
{
    public class ContestIndexVM
    {
        public ContestIndexVM()
        {
            Contests = new List<BAL.ViewModels.ContestViewModel>();
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
        }
        public List<BAL.ViewModels.ContestViewModel>? Contests { get; set; }
        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
    }
}
