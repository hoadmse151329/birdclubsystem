using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class ClubInformation
    {
        public int clubId { get; set; }
        public int? clubLocationId { get; set; }
        public string? description { get; set; }
    }
}
