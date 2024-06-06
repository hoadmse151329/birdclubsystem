using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Manager
{
    public class GetListStaffName : DefaultResponseViewModel<List<GetStaffName>>
    {
        public GetListStaffName()
        {
        }

        public GetListStaffName(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
