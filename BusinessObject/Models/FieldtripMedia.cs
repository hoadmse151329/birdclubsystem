using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class FieldtripMedia
    {
        public int pictureId { get; set; }
        public int? tripId { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
    }
}
