using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Admin
{
    public class GetAdminDashboard
    {
        public int TotalUsers { get; set; }
        public int TotalManagers { get; set; }
        public int TotalStaffs { get; set; }
    }
}
