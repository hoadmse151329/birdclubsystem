using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerMemberStatusIndexVM
    {
        public ManagerMemberStatusIndexVM()
        {
            MemberStatuses = new List<BAL.ViewModels.Manager.GetMemberStatus>();
            SelectedMemberStatuses = new List<string>();
        }
        public List<BAL.ViewModels.Manager.GetMemberStatus> MemberStatuses { get; set; }

        public List<string> SelectedMemberStatuses { get; set; }
    }
}
