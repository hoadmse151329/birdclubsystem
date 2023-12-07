using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class BirdMedia
    {
        public int pictureId { get; set; }
        public int? birdId { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
    }
}
