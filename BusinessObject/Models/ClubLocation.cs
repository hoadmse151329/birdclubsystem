using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class ClubLocation
    {
        public int clubLocationId { get; set; }
        public string? clubName { get; set; }
        public string? description { get; set; }
        public int? locationId { get; set; }
    }
}
