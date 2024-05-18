using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class CreateNotificationRequest
    {
        public string MemberId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
