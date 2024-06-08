using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerContestIndexVM
    {
        public ManagerContestIndexVM()
        {
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
            ContestList = new();
            CreateContest = new();
        }

        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
        public List<BAL.ViewModels.ContestViewModel> ContestList { get; set; }
        public BAL.ViewModels.Manager.CreateNewContestVM CreateContest { get; set; }

        public void SetCreateMeeting(BAL.ViewModels.Manager.CreateNewContestVM createNewContest, string? hostname, List<SelectListItem> staffnames)
        {
            CreateContest = createNewContest != null ? createNewContest : new();
            CreateContest.Host = hostname;
            CreateContest.StaffNames = staffnames;
        }
    }
}
