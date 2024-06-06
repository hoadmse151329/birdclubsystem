using BAL.ViewModels.Manager;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerMeetingIndexVM
    {
        public ManagerMeetingIndexVM()
        {
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
            MeetingList = new();
            CreateMeeting = new();
        }

        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
        public List<BAL.ViewModels.MeetingViewModel> MeetingList {  get; set; }
        public BAL.ViewModels.Manager.CreateNewMeetingVM CreateMeeting {  get; set; }

        public void SetCreateMeeting(CreateNewMeetingVM createNewMeeting, string? hostname, List<SelectListItem> staffnames)
        {
            CreateMeeting = createNewMeeting != null ? createNewMeeting : new();
            CreateMeeting.Host = hostname;
            CreateMeeting.StaffNames = staffnames;
        }
    }
}
