using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class NotificationViewModel
    {
        public string? NotificationId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
    }
}
