using BAL.ViewModels.Member;

namespace WebAppMVC.Models.Auth
{
    public class GetMemberFullNameResponse : DefaultResponseViewModel<MembershipRenewalRequest>
    {
        public GetMemberFullNameResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}