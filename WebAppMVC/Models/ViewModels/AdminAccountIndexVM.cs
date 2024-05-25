

namespace WebAppMVC.Models.ViewModels
{
    public class AdminAccountIndexVM
    {
        public AdminAccountIndexVM()
        {
            EmployeeStatuses = new List<BAL.ViewModels.Admin.GetEmployeeStatus>();
            SelectedEmployeeStatuses = new List<string>();
            createEmployee = new BAL.ViewModels.Admin.CreateNewEmployee();
        }
        public List<BAL.ViewModels.Admin.GetEmployeeStatus> EmployeeStatuses { get; set; }

        public List<string> SelectedEmployeeStatuses { get; set; }

        public BAL.ViewModels.Admin.CreateNewEmployee createEmployee { get; set; }
    }
}
