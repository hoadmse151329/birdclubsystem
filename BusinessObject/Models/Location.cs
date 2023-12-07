using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Location
    {
        public int locationId { get; set; }
        public string locationName { get; set; } = null!;
        public string description { get; set; } = null!;
    }
}
