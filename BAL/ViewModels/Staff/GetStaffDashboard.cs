using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Staff
{
    public class GetStaffDashboard
    {
        public int TotalEvents { get; set; }
        public int TotalEventsOpenRegistration { get; set; }
        public int TotalEventsClosedRegistration { get; set; }
        public int TotalEventsCheckingIn { get; set; }
        public int TotalEventsOngoing { get; set; }
    }
}
