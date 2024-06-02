namespace WebAppMVC.Models.ViewModels
{
    public class MemberProfileVM
    {
        public MemberProfileVM()
        {
            memberPassword = new BAL.ViewModels.Member.UpdateMemberPassword();
            memberDetail = new BAL.ViewModels.MemberViewModel();
        }

        public BAL.ViewModels.Member.UpdateMemberPassword memberPassword { get; set; }

        public BAL.ViewModels.MemberViewModel memberDetail { get; set; }
    }
}
