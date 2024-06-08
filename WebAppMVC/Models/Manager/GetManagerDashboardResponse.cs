using BAL.ViewModels.Event;

namespace WebAppMVC.Models.Manager
{
    public class GetManagerDashboardResponse : DefaultResponseViewModel<GetDashboardResponse>
    {
        public GetManagerDashboardResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
