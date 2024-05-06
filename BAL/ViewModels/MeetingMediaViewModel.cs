using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class MeetingMediaViewModel
    {
        public MeetingMediaViewModel()
        {
            Image = "/images/meeting.png";
        }
        public int? PictureId { get; set; }
        public int? MeetingId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
    }
}
