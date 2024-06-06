namespace WebAppMVC.Models.ViewModels
{
    public class AdminProfileVM
    {
        public AdminProfileVM()
        {
            adminPassword = new BAL.ViewModels.Member.UpdateMemberPassword();
            adminDetail = new BAL.ViewModels.MemberViewModel();
        }

        public BAL.ViewModels.Member.UpdateMemberPassword adminPassword { get; set; }

        public BAL.ViewModels.MemberViewModel adminDetail { get; set; }
    }
}
