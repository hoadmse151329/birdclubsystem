using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        // dùng format <tên quận>,<tên thành phố>
        public string LocationName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
