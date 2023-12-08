using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Feedback
    {
        public int feedbackId { get; set; }
        public int userId { get; set; }
        public string title { get; set; } = null!;
        public string? detail { get; set; }
        public DateTime? date { get; set; }
        public string? category { get; set; }
        public string? status { get; set; }
    }
}
