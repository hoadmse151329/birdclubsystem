namespace WebAppMVC.Models.ViewModels
{
    public class StaffProfileVM
    {
        public StaffProfileVM()
        {
            staffPassword = new BAL.ViewModels.Member.UpdateMemberPassword();
            staffDetail = new BAL.ViewModels.MemberViewModel();
        }

        public BAL.ViewModels.Member.UpdateMemberPassword staffPassword { get; set; }

        public BAL.ViewModels.MemberViewModel staffDetail { get; set; }
    }
}
