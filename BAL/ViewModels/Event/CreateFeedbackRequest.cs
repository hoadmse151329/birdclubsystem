using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class CreateFeedbackRequest
    {
        public string MemberId { get; set; }
        public string? EventId { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public string? Category { get; set; }
        public decimal? Rating { get; set; }
    }
}
