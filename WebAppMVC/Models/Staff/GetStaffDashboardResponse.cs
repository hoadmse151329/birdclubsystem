using BAL.ViewModels.Staff;

namespace WebAppMVC.Models.Staff
{
    public class GetStaffDashboardResponse : DefaultResponseViewModel<GetStaffDashboard>
    {
        public GetStaffDashboardResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
