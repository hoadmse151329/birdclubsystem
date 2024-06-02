using BAL.ViewModels.Manager;

namespace WebAppMVC.Models.Manager
{
    public class GetManagerDashboardResponse : DefaultResponseViewModel<GetManagerDashboard>
    {
        public GetManagerDashboardResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
