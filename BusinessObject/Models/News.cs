using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class News
    {
        public int newsId { get; set; }
        public string title { get; set; } = null!;
        public string category { get; set; } = null!;
        public string newsContent { get; set; } = null!;
        public DateTime uploadDate { get; set; }
        public string status { get; set; } = null!;
        public string? picture { get; set; }
        public string? filepdf { get; set; }
        public int userId { get; set; }
    }
}
