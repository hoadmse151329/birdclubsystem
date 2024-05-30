using BAL.ViewModels.Member;

namespace WebAppMVC.Models.Member
{
    public class GetMemberAvatarResponse : DefaultResponseViewModel<UpdateMemberAvatar>
    {
        public GetMemberAvatarResponse()
        {
        }

        public GetMemberAvatarResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
