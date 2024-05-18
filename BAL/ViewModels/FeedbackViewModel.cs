using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class FeedbackViewModel
    {
        public int? FeedbackId { get; set; }
        public int? UserId { get; set; }
        public string? EventId { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public DateTime? Date { get; set; }
        public string? Category { get; set; }
        public double? Rating { get; set; }
        public string? Status { get; set; }
    }
}
