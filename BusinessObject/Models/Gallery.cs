using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Gallery
    {
        public int? pictureId { get; set; }
        public string? description { get; set; }
        public int? userId { get; set; }
        public string image { get; set; } = null!;
    }
}
