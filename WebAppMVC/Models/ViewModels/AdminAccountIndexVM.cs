using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class AdminAccountIndexVM
    {
        public AdminAccountIndexVM()
        {
            EmployeeStatuses = new List<BAL.ViewModels.Admin.GetEmployeeStatus>();
            SelectedEmployeeStatuses = new List<string>();
        }
        public List<BAL.ViewModels.Admin.GetEmployeeStatus> EmployeeStatuses { get; set; }

        public List<string> SelectedEmployeeStatuses { get; set; }
    }
}
