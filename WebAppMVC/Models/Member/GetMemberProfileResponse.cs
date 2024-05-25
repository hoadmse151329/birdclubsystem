using BAL.ViewModels;

namespace WebAppMVC.Models.Member
{
    public class GetMemberProfileResponse : DefaultResponseViewModel<MemberViewModel>
    {
        public GetMemberProfileResponse()
        {
        }

        public GetMemberProfileResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
