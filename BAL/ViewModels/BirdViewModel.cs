using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModels
{
    public class BirdViewModel
    {
        public int? BirdId { get; set; }
        public int? MemberId { get; set; }
        public string? BirdName { get; set; }
        public int? Elo { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public DateTime? AddDate { get; set; }
        public string? ProfilePic { get; set; }
        public string? Status { get; set; }
        public string? Origin { get; set; }
    }
}
