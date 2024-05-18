using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Manager
{
    public class GetListMemberStatus : DefaultResponseViewModel<IEnumerable<GetMemberStatus>>
    {
        public GetListMemberStatus(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
