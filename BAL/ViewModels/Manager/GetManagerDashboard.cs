using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Manager
{
    public class GetManagerDashboard
    {
        public int TotalEvents { get; set; }
        public int TotalFeedbacks { get; set; }
        public int TotalBlogs { get; set; }
        public int TotalNews { get; set; }
        public int TotalIncome { get; set; }
        public List<GetFeedbackResponse> Feedbacks { get; set; }
    }
}
