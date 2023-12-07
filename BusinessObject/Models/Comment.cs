using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Comment
    {
        public int commentId { get; set; }
        public int? blogId { get; set; }
        public int? vote { get; set; }
        public string? description { get; set; }
        public DateTime? date { get; set; }
        public int? userId { get; set; }
    }
}
