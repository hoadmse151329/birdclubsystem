using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public partial class Member
    {
        public int memberId { get; set; }
        public string fullName { get; set; } = null!;
        public string userName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string gender { get; set; } = null!;
        public string role { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string description { get; set; } = null!;
        public string status { get; set; } = null!;
        public int? clubId { get; set; }
    }
}
