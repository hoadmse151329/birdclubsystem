using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Blog
    {
        public int blogId { get; set; }
        public int userId { get; set; }
        public string description { get; set; } = null!;
        public string? category { get; set; }
        public DateTime uploadDate { get; set; }
        public int vote { get; set; }
        public string? image { get; set; }
        public string status { get; set; } = null!;
    }
}
