using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels.Event
{
    public class NotificationResponse
    {
        public NotificationResponse()
        {
            NotificationCount = 0;
            NotificationTitle = null;
        }
        public int NotificationCount { get; set; }
        public List<string>? NotificationTitle { get; set; }
    }
}
