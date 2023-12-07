using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Users
    {
        public int userId { get; set; }
        public int? clubId { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public int? memberId { get; set; }
    }
}
