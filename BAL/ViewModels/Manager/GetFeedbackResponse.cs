using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Manager
{
    public class GetFeedbackResponse
    {
        public decimal? Rating { get; set; }
        public string? Fullname { get; set; }
        public string? Details { get; set; }
        public DateTime? Date { get; set; }
    }
}
