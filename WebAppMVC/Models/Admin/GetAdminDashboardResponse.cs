using BAL.ViewModels.Admin;

namespace WebAppMVC.Models.Admin
{
    public class GetAdminDashboardResponse : DefaultResponseViewModel<GetAdminDashboard>
    {
        public GetAdminDashboardResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
