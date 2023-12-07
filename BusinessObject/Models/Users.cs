using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Users
    {
        public string userId { get; set; } = null!;
        public string? clubId { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? memberId { get; set; }
    }
}
