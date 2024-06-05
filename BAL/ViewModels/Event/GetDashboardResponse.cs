using BAL.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class GetDashboardResponse
    {
        private List<GetFeedbackResponse> feedbacks;

        public int TotalEvents { get; set; }
        public int TotalFeedbacks { get; set; }
        public int TotalBlogs { get; set; }
        public int TotalNews { get; set; }
        public int TotalIncome { get; set; }
        public List<GetFeedbackResponse> Feedbacks { get; set; }
    }
}
