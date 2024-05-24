

using BAL.ViewModels.Admin;

namespace WebAppMVC.Models.Admin
{
    public class GetListEmployeeStatus : DefaultResponseViewModel<List<GetEmployeeStatus>>
    {
        public GetListEmployeeStatus(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
