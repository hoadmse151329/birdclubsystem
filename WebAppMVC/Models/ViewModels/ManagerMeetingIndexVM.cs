using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerMeetingIndexVM
    {
        public ManagerMeetingIndexVM()
        {
            locationList = new List<string>();
            meetingList = new();
            createMeeting = new();
            cancelMeet = new();
        }

        public List<string>? locationList {  get; set; }
        /*public List<BAL.ViewModels.MemberViewModel>? staffList { get; set; }
        public BAL.ViewModels.MemberViewModel? selectedStaff { get; set; }
        public List<SelectListItem> DefaultStaffSelectableList { get; set; }*/
        public List<BAL.ViewModels.MeetingViewModel>? meetingList {  get; set; }
        public BAL.ViewModels.MeetingViewModel? createMeeting {  get; set; }
        public BAL.ViewModels.MeetingViewModel? cancelMeet {  get; set; }
    }
}
