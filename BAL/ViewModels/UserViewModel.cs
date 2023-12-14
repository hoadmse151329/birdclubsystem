using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class UserViewModel
    {
        public int? UserId { get; set; }
        public int? ClubId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? MemberId { get; set; }
    }
}
