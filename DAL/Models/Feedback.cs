using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Detail { get; set; }
        public DateTime? Date { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
