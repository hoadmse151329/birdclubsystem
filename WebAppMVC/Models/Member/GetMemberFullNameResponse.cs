using BAL.ViewModels.Member;

namespace WebAppMVC.Models.Member
{
    public class GetMemberFullNameResponse : DefaultResponseViewModel<MembershipRenewalRequest>
    {
        public GetMemberFullNameResponse()
        {
        }

        public GetMemberFullNameResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
