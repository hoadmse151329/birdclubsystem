using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldtripGettingThere
    {
        public int tripId { get; set; }
        public int gettingThereId { get; set; }
        public string gettingThereNext { get; set; } = null!;
    }
}
