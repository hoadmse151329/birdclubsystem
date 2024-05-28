namespace WebAppMVC.Models.ViewModels
{
    public class MemberFieldTripIndexVM
    {
        public MemberFieldTripIndexVM()
        {
            FieldTrips = new List<BAL.ViewModels.FieldTripViewModel>();
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
        }
        public List<BAL.ViewModels.FieldTripViewModel>? FieldTrips { get; set; }
        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
    }
}
