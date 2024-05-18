using BAL.ViewModels;

namespace WebAppMVC.Models.Manager
{
    public class GetMemberStatusExpireResponse : DefaultResponseViewModel<MemberViewModel>
    {
        public GetMemberStatusExpireResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
