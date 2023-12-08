using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldtripDaybyDay
    {
        public int tripId { get; set; }
        public int daybyDayID { get; set; }
        public int day { get; set; }
        public string description { get; set; } = null!;
        public string? pictureId { get; set; }
    }
}
