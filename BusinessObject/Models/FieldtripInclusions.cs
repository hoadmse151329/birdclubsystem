using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldtripInclusions
    {
        public int tripId { get; set; }
        public int inclusionId { get; set; }
        public string title { get; set; } = null!;
        public string inclusionText { get; set; } = null!;
        public string type { get; set; } = null!;
    }
}
