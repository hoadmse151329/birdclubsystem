using BAL.ViewModels;

namespace WebAppMVC.Models.Member
{
    public class GetMemberProfileResponse : DefaultResponseViewModel
    {
        public MemberViewModel? Data { get; set; }
    }
}
