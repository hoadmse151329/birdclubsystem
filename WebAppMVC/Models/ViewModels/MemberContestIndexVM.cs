using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class MemberContestIndexVM
    {
        public MemberContestIndexVM()
        {
            Contests = new List<BAL.ViewModels.ContestViewModel>();
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
            SelectedReqEloRange = new List<SelectListItem>() {
                new SelectListItem { Text = Constants.Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.Constants.REQUIRED_ELO_RANGE_DEFAULT, Selected = true },
                new SelectListItem { Text = Constants.Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.Constants.REQUIRED_ELO_RANGE_BELOW_1000 },
                new SelectListItem { Text = Constants.Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.Constants.REQUIRED_ELO_RANGE_1000_TO_1500 },
                new SelectListItem { Text = Constants.Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.Constants.REQUIRED_ELO_RANGE_1500_TO_2000 },
                new SelectListItem {Text = Constants.Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.Constants.REQUIRED_ELO_RANGE_ABOVE_2000 }
            };
        }
        public List<BAL.ViewModels.ContestViewModel>? Contests { get; set; }
        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
        public List<SelectListItem> SelectedReqEloRange { get; set; }
    }
}
