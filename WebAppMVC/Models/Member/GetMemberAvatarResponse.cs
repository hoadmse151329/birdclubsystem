using BAL.ViewModels.Member;

namespace WebAppMVC.Models.Member
{
    public class GetMemberAvatarResponse : DefaultResponseViewModel
    {
        public UpdateMemberAvatar? Data { get; set; }
    }
}
